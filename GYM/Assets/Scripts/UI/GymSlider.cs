using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HappyBat
{
    public class GymSlider : MonoBehaviour
    {
        [SerializeField] float _maxValue = 5000f;
        [SerializeField] float _decreaseAmount = 200f;
        [SerializeField] int _rewardAmountPerBar = 5;
        [SerializeField] GymMachine _gymMachine;

        public Actor actor;

        public Image image;
        float _currentSliderPoints;
        [SerializeField] float _increaseStep = 50;


        

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

            var reward = (int)(currentFillPoints * _rewardAmountPerBar);

            return reward;
        }
    }

}
