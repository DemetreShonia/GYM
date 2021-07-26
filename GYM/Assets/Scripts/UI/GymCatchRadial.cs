using UnityEngine;
using UnityEngine.UI;

namespace HappyBat
{
    public class GymCatchRadial : GymUiBase
    {
        [Header("References")]
        [SerializeField] RectTransform _MovingImageRect; //transforms
        [SerializeField] RectTransform _targetImageRect;


        [Header("Values")]
        [SerializeField] float _moveSpeed; //is witeli ragac rom dadis zemot qvemot
        [SerializeField] float _maxAngle; //is witeli ragac rom dadis zemot qvemot
        [SerializeField] float _catchArea = 10f; //is witeli ragac rom dadis zemot qvemot


        bool _isRotatingRight = true; //is witeli ragac rom dadis zemot qvemot
        bool _shouldMove = true; //is witeli ragac rom dadis zemot qvemot




        void Start()
        {
            RandomizeTargetRot();
        }

        void Update()
        {
            if (_shouldMove)
            {
                RotateMovingPart();
                ChangeMovingPartDir();
            }

            if (Input.GetMouseButtonDown(0))
            {
                CheckIfCaught();
            }
        }

        void RotateMovingPart()
        {
            if (_isRotatingRight)
            {
                _MovingImageRect.transform.Rotate(Vector3.back * Time.deltaTime * _moveSpeed);
            }
            else
            {
                _MovingImageRect.transform.Rotate(Vector3.forward * Time.deltaTime * _moveSpeed);
            }
        }
        public static Vector3 GetSignedEulerAngles(Vector3 angles)
        {
            Vector3 signedAngles = Vector3.zero;
            for (int i = 0; i < 3; i++)
            {
                signedAngles[i] = (angles[i] + 180f) % 360f - 180f;
            }
            return signedAngles;
        }

        void ChangeMovingPartDir()
        {
            var movingImageRot = GetSignedEulerAngles(_MovingImageRect.transform.eulerAngles);

            if (_isRotatingRight)
            {
                if (movingImageRot.z < -_maxAngle)
                {
                    _isRotatingRight = false;
                }
            }
            else
            {
                if (movingImageRot.z > _maxAngle)
                {
                    _isRotatingRight = true;
                }
            }

        }
        void RandomizeTargetRot()
        {
            var targetRot = _targetImageRect.transform.localEulerAngles;

            var randRotZ = Random.Range(_maxAngle, -_maxAngle);

            _targetImageRect.transform.rotation = Quaternion.Euler(targetRot.x, targetRot.y, randRotZ);
        }
        void CheckIfCaught() // tu moartka ra
        {
            _shouldMove = false;

            var t1 = _MovingImageRect.localEulerAngles.z;
            var t2 = _targetImageRect.localEulerAngles.z;

            if(Mathf.Abs(t1 - t2) < _catchArea)
            {
               print("CATCH");
                _gymMachine.Train(rewardAmount);
            }

        }
    }
}

