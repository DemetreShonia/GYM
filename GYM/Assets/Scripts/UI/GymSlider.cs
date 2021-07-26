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

        // Update is called once per frame
        void Update()
        {
            CheckInput();
            CalculateCurrentSliderPoints();
            UpdateUI();
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

                _gymMachine.Train(CalculateWorkOutReward());
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
