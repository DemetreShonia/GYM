using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HappyBat
{
    public class Actor : MonoBehaviour
    {
        ActorAnimator _actorAnimator;
        ActorMovement _actorMovement;

        [SerializeField] GymSlider _gymSlider;

        #region Points

        [SerializeField] float maxLegsPoints;
        [SerializeField] float maxHandPoints;
        [SerializeField] float maxStaminaPoints;
        [SerializeField] float maxHealthPoints;

        [SerializeField] Image legsPercentImage;
        [SerializeField] Image handPercentImage;
        [SerializeField] Image staminaPercentImage;
        [SerializeField] Image healPercentImage;

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
                    return legsPoints / maxLegsPoints;
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

        private void Start()
        {
            _actorAnimator = GetComponent<ActorAnimator>();
            _actorMovement = GetComponent<ActorMovement>();
            SetFillAmountsToZero();
        }
        public void SitOn(Transform gymMachineT, int workOutId)
        {
            // aq animacia unda gaeshvas romeli?
            // jdomis poza ra
            _actorMovement.SitOnGymMachine();
            _actorAnimator.PrepareForWorkOut(workOutId);

            transform.position = gymMachineT.position;
            transform.rotation = gymMachineT.rotation;
            transform.SetParent(gymMachineT);

        }
        void SetFillAmountsToZero()
        {
           //legsPercentImage.fillAmount = 0;
          //  handPercentImage.fillAmount = 0;
            staminaPercentImage.fillAmount = 0;
            healPercentImage.fillAmount = 0;
        }
        public void StandUp(Vector3 newPos)
        {
            transform.SetParent(null);
           // transform.position = position;

            _actorMovement.enabled = true;

            _actorAnimator.StopWorkOut();

            //  _actorMovement.SitUpFromGymMachine(newPos);
            _actorMovement.SitUpFromGymMachine(newPos);

        }
        bool isOnTreadmill = false;
        int _rewardAmount;
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
                    print(amount);
                    isOnTreadmill = true;
                    _rewardAmount = amount;

                    staminaPoints += _rewardAmount;
                    staminaPercentImage.fillAmount = currentStaminaPercent;

                    float animPercent = _gymSlider.imagePercent;

                    _actorAnimator.WorkOutWithSlider(animPercent); 


                    break;
                case WorkOutType.Health:
                    healthPoints += amount;
                    _actorAnimator.WorkOut();
                    healPercentImage.fillAmount = currentHealthPercent;
                    print(healthPoints);
                    break;
            }
        
            // tavisit ro qnas
        //private void Update()
        //{
        //    if (isOnTreadmill)
        //    {
        //        staminaPoints += _rewardAmount;
        //        float animPercent = _gymSlider.imagePercent;

        //        _actorAnimator.WorkOutWithSlider(animPercent);
        //    }
        }

    }

}
