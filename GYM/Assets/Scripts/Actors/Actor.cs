using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class Actor : MonoBehaviour
    {
        //  public int CurrentGymPoints { get; private set; }

        //public void UpdateGymPoints(int amount)
        //{
        //    CurrentGymPoints += amount;
        //    if (CurrentGymPoints < 0)
        //        CurrentGymPoints = 0;
        //}

        #region Points

        [SerializeField] float maxLegsPoints;
        [SerializeField] float maxHandPoints;

        float _legsPoints;
        float _leftHandPoints;
        float _rightHandPoints;

        public float legsPoints
        {
            get
            {
                return _legsPoints;
            }
            set
            {
                if(value > maxLegsPoints)
                    _legsPoints = maxLegsPoints;
                else if(value < 0)
                    _legsPoints = 0;
                else
                    _legsPoints = value;
            }
        }
        public float leftHandPoints
        {
            get
            {
                return _leftHandPoints;
            }
            set
            {
                if (value > maxHandPoints)
                    _leftHandPoints = maxHandPoints;
                else if (value < 0)
                    _leftHandPoints = 0;
                else
                    _leftHandPoints = value;
            }
        }
        public float rightHandPoints
        {
            get
            {
                return _rightHandPoints;
            }
            set
            {
                if (value > maxHandPoints)
                    _rightHandPoints = maxHandPoints;
                else if (value < 0)
                    _rightHandPoints = 0;
                else
                    _rightHandPoints = value;
            }
        }

        public float currentLegsPointsPercent
        {
            get
            {
                if (legsPoints == 0)
                    return 0;
                else
                    return legsPoints / maxHandPoints;
            }
        }
        public float currentLeftHandPercent
        {
            get
            {
                if (leftHandPoints == 0)
                    return 0;
                else
                    return leftHandPoints / maxHandPoints;
            }
        }
        public float currentRightHandPercent
        {
            get
            {
                if (rightHandPoints == 0)
                    return 0;
                else
                    return rightHandPoints / maxHandPoints;

            }
        }

        #endregion

        public void SitOn(Vector3 position)
        {

        }
        public void WorkOut(WorkOutType workOutType, int amount)
        {
            switch (workOutType)
            {
                case WorkOutType.Legs:
                    legsPoints += amount;
                    print(currentLegsPointsPercent);
                    break;
                case WorkOutType.LeftHand:
                    leftHandPoints += amount;
                    break;
                case WorkOutType.RightHand:
                    rightHandPoints += amount;
                    break;
            }
        }
    }

}
