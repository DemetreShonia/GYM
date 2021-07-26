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
        [SerializeField] float maxStaminaPoints;
        [SerializeField] float maxHealthPoints;

        float _legsPoints; // SPEED
        float _leftHandPoints; // POWER
        float _rightHandPoints; // POWER
        float _staminaPoints; // Stamina
        float _healthPoints; // sicocxle default 100 daakene!

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
        public float staminaPoints
        {
            get
            {
                return _staminaPoints;
            }
            set
            {
                if (value > maxStaminaPoints)
                    _staminaPoints = maxStaminaPoints;
                else if (value < 0)
                    _staminaPoints = 0;
                else
                    _staminaPoints = value;
            }
        }
        public float healthPoints
        {
            get
            {
                return _healthPoints;
            }
            set
            {
                if (value > maxHealthPoints)
                    _healthPoints = maxHealthPoints;
                else if (value < 0)
                    _healthPoints = 0;
                else
                    _healthPoints = value;
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
        public float currentStaminaPercent
        {
            get
            {
                if (staminaPoints == 0)
                    return 0;
                else
                    return staminaPoints / maxStaminaPoints;

            }
        }
        public float currentHealthPercent  // es gvinda vabshe?
        {
            get
            {
                if (healthPoints == 0)
                    return 0;
                else
                    return healthPoints / maxHealthPoints;
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
                    break;
                case WorkOutType.LeftHand:
                    leftHandPoints += amount;
                    break;
                case WorkOutType.RightHand:
                    rightHandPoints += amount;
                    break;
                case WorkOutType.Stamina:
                    staminaPoints += amount;
                    break;
                case WorkOutType.Health:
                    print(healthPoints);
                    healthPoints += amount;
                    print(healthPoints);
                    break;
            }
        }
    }

}
