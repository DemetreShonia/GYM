using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorMovement : MonoBehaviour
{
    Vector3 _inputValue;
    float inputSqrMagnitude;
    public float speed;
    NavMeshAgent _navMeshAgent;
    CapsuleCollider _capsuleCollider;

    public bool isMoving { get; private set; }
    bool _shouldMove = true;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldMove)
        {
            CheckInput();
            Move();
        }
    }
    public void SitOnGymMachine()
    {

        _navMeshAgent.enabled = false;
        _capsuleCollider.enabled = false;
        _shouldMove = false;
    }
    public void SitUpFromGymMachine(Vector3 newPos)
    {
        _navMeshAgent.enabled = true;
        _capsuleCollider.enabled = true;
        var t= _navMeshAgent.Warp(newPos);
        _shouldMove = true;
    }
    void CheckInput()
    {
        _inputValue.x = Input.GetAxis("Horizontal");
        _inputValue.z = Input.GetAxis("Vertical");
    }
    void LookAtMovingDir(Vector3 dir)
    {
        transform.rotation = Quaternion.LookRotation(dir);
    }
    void Move()
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

            Vector3 newPos = transform.position + desiredDir * Time.deltaTime * speed;
            NavMeshHit hit;
            bool isValid = NavMesh.SamplePosition(newPos, out hit, 5f, NavMesh.AllAreas);

            if (isValid)
            {
                if ((transform.position - hit.position).magnitude >= 0.2f)
                {
                    _navMeshAgent.Move(desiredDir * speed * Time.deltaTime);
                    isMoving = true;
                }
            }
        }
        else
        {
            isMoving = false;
        }
    }
    
}
