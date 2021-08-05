using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class DiceTexts : MonoBehaviour
    {
        [SerializeField] TextMesh _firstDiceText;
        [SerializeField] TextMesh _secondDiceText;
        [SerializeField] TextMesh _sumDiceText;
        Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void ShowText(int firstDiceAmount, int secondDiceAmount)
        {
            var sum = firstDiceAmount + secondDiceAmount;

            _firstDiceText.text = firstDiceAmount.ToString();
            _secondDiceText.text = secondDiceAmount.ToString();
            _sumDiceText.text = sum.ToString();
            _animator.SetTrigger("ShowResultText");

        }
    }
}

