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

    public bool isMoving { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Move();
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
