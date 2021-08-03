using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HappyBat
{
    public class WorkOutData
    {
        bool _isFinishedLeftHand;
        bool _isFinishedRightHand;
        bool _isFinishedLegs;
        bool _isFinishedStamina;
        bool _isFinishedHealth;
        public bool HasFinished(WorkOutType workOutType)
        {
            switch (workOutType)
            {
                case WorkOutType.Legs:
                    return _isFinishedLegs;
                case WorkOutType.LeftHand:
                    return _isFinishedLeftHand;
                case WorkOutType.RightHand:
                    return _isFinishedRightHand;
                case WorkOutType.Stamina:
                    return _isFinishedStamina;
                case WorkOutType.Health:
                    return _isFinishedHealth;
                default:
                    return false;
            }
        }
        public void Finish (WorkOutType workOutType)
        {
            switch (workOutType)
            {
                case WorkOutType.Legs:
                    _isFinishedLegs = true;
                    break;
                case WorkOutType.LeftHand:
                    _isFinishedLeftHand = true;
                    break;
                case WorkOutType.RightHand:
                    _isFinishedRightHand = true;
                    break;
                case WorkOutType.Stamina:
                    _isFinishedStamina = true;
                    break;
                case WorkOutType.Health:
                    _isFinishedHealth = true;
                    break;
            }
        }
    }

    public class Actor : MonoBehaviour
    {
        ActorAnimator _actorAnimator;
        ActorMovement _actorMovement;


        public WorkOutData workOutData = new WorkOutData(); // new?
        public bool isPlayer;

        #region Points
        [Header("References")]
        [SerializeField] GymSlider _gymSlider;

        [SerializeField] Image legsPercentImage;
        [SerializeField] Image handPercentImage;
        [SerializeField] Image staminaPercentImage;
        [SerializeField] Image healPercentImage;

        [Header("Values")]

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
            _actorMovement.SitOnSlotMachine();
            _actorAnimator.PrepareForWorkOut(workOutId);

            transform.position = gymMachineT.position;
            transform.rotation = gymMachineT.rotation;
            transform.SetParent(gymMachineT);

        }
        public void SitOnSlotMachine()
        {
            _actorMovement.SitOnSlotMachine();
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
            _isWorkingOut = false;
            transform.SetParent(null);
           // transform.position = position;

            _actorMovement.enabled = true;

            _actorAnimator.StopWorkOut();

            //  _actorMovement.SitUpFromGymMachine(newPos);
            _actorMovement.SitUpFromGymMachine(newPos);

            _actorAnimator.WorkOutWithSlider(0f);

        }
        public void WorkOut(WorkOutType workOutType, int amount)
        {
            
            _isWorkingOut = true;
            _workOutType = workOutType;
            _rewardAmount = amount;


            switch (workOutType)
            {
                case WorkOutType.Legs:
                    if (currentLegsPointsPercent >= 0.93f)
                    {
                        workOutData.Finish(workOutType);
                    }
                    else
                    {
                        legsPoints += amount;
                    }
                    break;
                case WorkOutType.LeftHand:
                    if (currentLeftHandPercent >= 0.93f)
                    {
                        workOutData.Finish(workOutType);
                    }
                    else
                    {
                        leftHandPoints += amount;
                    }
                    break;

                case WorkOutType.RightHand:
                    if (currentRightHandPercent >= 0.93f)
                    {
                        workOutData.Finish(workOutType);
                    }
                    else
                    {
                        rightHandPoints += amount;
                    }

                    break;
                case WorkOutType.Stamina:
                    if (currentStaminaPercent >= 0.93f)
                    {
                        workOutData.Finish(workOutType);
                        _isWorkingOut = false;
                    }
                    else
                    {
                        _rewardAmount = amount;
                    }
                    break;
                case WorkOutType.Health:
                    if(currentHealthPercent >= 0.93f)
                    {
                        workOutData.Finish(workOutType);
                    }
                    else
                    {
                        healthPoints += amount;
                        _actorAnimator.WorkOut();
                        healPercentImage.fillAmount = currentHealthPercent;
                        print(healthPoints);
                    }
                    
                    break;
            }
        }


        int _rewardAmount;
        bool _isWorkingOut = true;
        WorkOutType _workOutType;
        float _timer;
        float _maxTime = 1f;

        private void Update()
        {
            if (_isWorkingOut)
            {
                switch (_workOutType)
                {
                    case WorkOutType.Stamina:
                        if (workOutData.HasFinished(_workOutType))
                            return;

                        UpdateStaminaWorkOut();
                        break;
                }
                
            }
        }
        void UpdateStaminaWorkOut()
        {
            float runPercent;
            if (isPlayer)
            {
                runPercent = _gymSlider.imagePercent;
            }
            else
            {
                runPercent = .5f;
            }

            _actorAnimator.WorkOutWithSlider(runPercent);
           // print(_rewardAmount * runPercent) ;

            _timer += Time.deltaTime;

            if (_timer > _maxTime)
            {

                staminaPoints += _rewardAmount * runPercent;

                print(staminaPoints);
                staminaPercentImage.fillAmount = currentStaminaPercent;
                _timer = 0;
            }
        }
    }

}
