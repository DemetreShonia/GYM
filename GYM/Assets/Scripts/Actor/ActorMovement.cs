using UnityEngine;
using UnityEngine.AI;

namespace HappyBat
{
    public class ActorMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] FloatingJoystick _dynamicJoystick;
        [Header("Values")]
        [SerializeField] float _speed; // ser fild


        Vector3 _inputValue;
        NavMeshAgent _navMeshAgent;
        CapsuleCollider _capsuleCollider;
        Actor _actor;

        bool _shouldMove = true;

        float inputSqrMagnitude;

        public bool isMoving { get; private set; }
        // Start is called before the first frame update

        #region BOTH
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _actor = GetComponent<Actor>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_shouldMove)
            {
                if (_actor.isPlayer)
                {
                    CheckInput();
                    MovePlayer();
                }
                else
                {
                    if (_navMeshAgent.velocity.magnitude < 0.2f)
                    {
                        MoveAI();
                    }
                }
            }
        }

        public void SitOnSlotMachine()
        {
            _navMeshAgent.enabled = false;
            _capsuleCollider.enabled = false;
            _shouldMove = false;
            isMoving = false;
        }
        //public void SitUpFromGymMachine(Vector3 newPos)
        //{
        //    _navMeshAgent.enabled = true;
        //    _capsuleCollider.enabled = true;
        //    var t = _navMeshAgent.Warp(newPos);
        //    _shouldMove = true;
        //}

        public void StandUpFromSlotMachine(Vector3 newPos)
        {
            _navMeshAgent.enabled = true;
            _capsuleCollider.enabled = true;
            var t = _navMeshAgent.Warp(newPos);
            _shouldMove = true;
        }

        #endregion

        #region Player
        void LookAtMovingDir(Vector3 dir)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
        void CheckInput()
        {
            //_inputValue.x = Input.GetAxis("Horizontal");
            //_inputValue.z = Input.GetAxis("Vertical");
            _inputValue.x = _dynamicJoystick.Direction.x;
            _inputValue.z = _dynamicJoystick.Direction.y;
        }
        void MovePlayer()
        {
            inputSqrMagnitude = _inputValue.sqrMagnitude;

            if (inputSqrMagnitude >= .01f)
            {
                // move relative to camera
                var forward = Camera.main.transform.forward;
                var right = Camera.main.transform.right;

                forward.y = 0;
                right.y = 0;
                forward.Normalize();
                right.Normalize();

                var desiredDir = forward * _inputValue.z + right * _inputValue.x;
                // move relative to camera
                LookAtMovingDir(desiredDir);

                Vector3 newPos = transform.position + desiredDir * Time.deltaTime * _speed;
                NavMeshHit hit;
                bool isValid = NavMesh.SamplePosition(newPos, out hit, 5f, NavMesh.AllAreas);

                if (isValid)
                {
                    if ((transform.position - hit.position).magnitude >= 0.2f)
                    {
                        _navMeshAgent.Move(desiredDir * _speed * Time.deltaTime);
                        isMoving = true;
                    }
                }
            }
            else
            {
                isMoving = false;
            }
        }

        #endregion

        #region AI

        void MoveAI()
        {
            RandomizeWayPoint();

            var wayPoint = RandomizeWayPoint();

            if (wayPoint.CompareTag("WorkOutMachine"))
            {
                var gymMachine = wayPoint.GetComponentInChildren<GymMachine>();

                if (gymMachine != null && gymMachine.amIAvailable)
                {
                    _navMeshAgent.SetDestination(wayPoint.position);
                }
            }
            else
            {
                _navMeshAgent.SetDestination(wayPoint.position);
                //   RandomizeGymMachineToGo(); // ??
            }
            isMoving = true;
        }
        Transform RandomizeWayPoint()
        {
            // BOT ONLY
             var wayPoints = WayPointsManager.instance.wayPoints;

             var randID = Random.Range(0, wayPoints.Length); // es sheidzleba singletonshi gavides

             return wayPoints[randID];
        }

        #endregion

        

    }

}
