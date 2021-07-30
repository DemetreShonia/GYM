using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace HappyBat
{
    public class GymCatchRect : GymUiBase
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



        private void Awake()
        {
            _gymMachine.uiInputGO = gameObject;
        }
        
        IEnumerator ResetEveryThing(float delay)
        {
            yield return new WaitForSeconds(delay);
            RandomizeTargetPos();
            _shouldMove = true;
        }
        public override void Reset()
        {
            if(gameObject.activeSelf && this.enabled)
                StartCoroutine(ResetEveryThing(.2f)); // 1 wami moicdis
        }
        public override void SitOn()
        {
            RandomizeTargetPos();
            _shouldMove = true;
        }

        void Update()
        {
            if (_shouldMove)
            {
                TranslateMovingPart();
                ChangeMovingPartDir();
                CheckIfCaught();
            }
        }

        void TranslateMovingPart()
        {
            if (_isMovingUp)
            {
                _MovingImageRect.transform.localPosition += (_upBorderT.localPosition - _MovingImageRect.localPosition).normalized * _MoveSpeed * Time.deltaTime;
            }
            else
            {
                _MovingImageRect.transform.localPosition += (_downBorderT.localPosition - _MovingImageRect.localPosition).normalized * _MoveSpeed * Time.deltaTime;
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
            Vector3 newPos = _downBorderT.localPosition.GetRandomVector3Between(_upBorderT.localPosition);

            _targetImageRect.transform.localPosition = newPos ;
        }
        
        void CheckIfCaught() // tu moartka ra
        {

            if (Input.GetMouseButtonDown(0))
            {
                _shouldMove = false;
                var cought = _targetImageRect.CheckIfRectsOverlap(_MovingImageRect);

                if (cought)
                {
                    _gymMachine.Train(rewardAmount);
                }
                Reset();
            }
        }
    }
}

