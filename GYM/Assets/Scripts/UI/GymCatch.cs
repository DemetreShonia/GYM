using UnityEngine;
using UnityEngine.UI;

namespace HappyBat
{
    public class GymCatch : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] RectTransform _MovingImageRect;
        [SerializeField] RectTransform _targetImageRect;
        [SerializeField] Transform _upBorderT;
        [SerializeField] Transform _downBorderT;

        [Header("Values")]
        [SerializeField] float _MoveSpeed; //is witeli ragac rom dadis zemot qvemot


        bool _isMovingUp = true; //is witeli ragac rom dadis zemot qvemot
        bool _shouldMove = true; //is witeli ragac rom dadis zemot qvemot




        void Start()
        {
            RandomizeTargetPos();
        }

        void Update()
        {
            if (_shouldMove)
            {
                TranslateMovingPart();
                ChangeMovingPartDir();
            }
            CheckIfCaught();
        }

        void TranslateMovingPart()
        {
            if (_isMovingUp)
            {
                _MovingImageRect.transform.position += Vector3.up * _MoveSpeed * Time.deltaTime;
            }
            else
            {
                _MovingImageRect.transform.position += Vector3.down * _MoveSpeed * Time.deltaTime;
            }
        }
        void ChangeMovingPartDir()
        {
            var movingImagePos = _MovingImageRect.transform.position;

            if (movingImagePos.y > _upBorderT.position.y)
            {
                _MovingImageRect.transform.position = new Vector3(movingImagePos.x, _upBorderT.position.y, movingImagePos.z);
                _isMovingUp = false;
            }
            else if(movingImagePos.y < _downBorderT.position.y)
            {
                _MovingImageRect.transform.position = new Vector3(movingImagePos.x, _downBorderT.position.y, movingImagePos.z);
                _isMovingUp = true;
            }

        }
        void RandomizeTargetPos()
        {
            var targetPos = _targetImageRect.transform.position;

            var randYPos = Random.Range(_downBorderT.position.y, _upBorderT.position.y);

            Vector3 newPos = new Vector3(targetPos.x, randYPos, targetPos.z);

            _targetImageRect.transform.position = newPos;
        }
        void CheckIfCaught() // tu moartka ra
        {
            if (Input.GetMouseButtonDown(0))
            {
                _shouldMove = false;
                var t = _targetImageRect.CheckIfRectsOverlap(_MovingImageRect);
                print(t);
            }
        }
    }
}

