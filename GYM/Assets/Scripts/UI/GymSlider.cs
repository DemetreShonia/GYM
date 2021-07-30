using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HappyBat
{
    public class GymSlider : GymUiBase
    {
        [Header("Values")]
        [SerializeField] float _maxValue = 5000f;
        [SerializeField] float _increaseStep = 50;
        [SerializeField] float _decreaseAmount = 200f;

        [Header("References")]
        [SerializeField] Image image;

        float _currentSliderPoints;
        public float imagePercent
        {
            get
            {
                return _currentSliderPoints / _maxValue;
            }
        }

        private void Awake()
        {
            _gymMachine.uiInputGO = gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            CheckInput();
            CalculateCurrentSliderPoints();
            UpdateUI();
        }
        public override void Reset()
        {
            _currentSliderPoints = 0;
        }

        public override void SitOn()
        {
            _currentSliderPoints = 0;
        }
        void UpdateUI()
        {
            image.fillAmount = 1 - (_currentSliderPoints / _maxValue);
        }
        void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _currentSliderPoints += _increaseStep;

                _gymMachine.Train(rewardAmount);
            }
        }
        void CalculateCurrentSliderPoints()
        {
            _currentSliderPoints -= Time.deltaTime * _decreaseAmount;
            _currentSliderPoints = Mathf.Clamp(_currentSliderPoints, 0, _maxValue);
        }
        int CalculateWorkOutReward()
        {
            var currentFillPoints = (1 - image.fillAmount) * 10;

            var reward = (int)(currentFillPoints * rewardAmount);

            return reward;
        }
    }

}
