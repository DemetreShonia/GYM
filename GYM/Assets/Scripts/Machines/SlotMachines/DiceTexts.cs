using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace HappyBat
{
    public class DiceTexts : MonoBehaviour
    {
        [SerializeField] TextMesh _firstDiceText;
        [SerializeField] TextMesh _secondDiceText;
        [SerializeField] TextMesh _sumDiceText;

        [SerializeField] Transform _firstDiceTransform;
        [SerializeField] Transform _secondDiceTransform;

        [SerializeField] CandyMachine _candyMachine;

        Vector3 _sumTextStartPos;

        int _diceSum;

        private void Awake()
        {
            DisableTexts();

            _sumTextStartPos = _sumDiceText.transform.position;
        }
        public void ShowText(int firstDiceAmount, int secondDiceAmount)
        {
            EnableTexts();

            var sum = firstDiceAmount + secondDiceAmount;
            _diceSum = sum;

            _firstDiceText.text = firstDiceAmount.ToString();
            _secondDiceText.text = secondDiceAmount.ToString();
            _sumDiceText.text = sum.ToString();

            _firstDiceText.transform.DOJump(_sumDiceText.transform.position, 1, 1, .2f);
            
            Sequence seq = DOTween.Sequence();

            seq.Insert(0f, _firstDiceText.transform.DOJump(_sumDiceText.transform.position, 1, 1, .6f));
            seq.Insert(0f, _firstDiceText.transform.DOScale(0.03f, 0.6f));
            seq.Insert(0f, _secondDiceText.transform.DOScale(0.03f, 0.6f));
            seq.Insert(0f, _secondDiceText.transform.DOJump(_sumDiceText.transform.position, 1, 1, .6f));
            seq.OnComplete(BounceSumText);
        }

        void EnableTexts()
        {
            _firstDiceText.transform.position = _firstDiceTransform.position;
            _secondDiceText.transform.position = _secondDiceTransform.position;

            _firstDiceText.gameObject.SetActive(true);
            _secondDiceText.gameObject.SetActive(true);

            _sumDiceText.gameObject.SetActive(false);

        }
        void DisableTexts()
        {
            _firstDiceText.gameObject.SetActive(false);
            _secondDiceText.gameObject.SetActive(false);

            _sumDiceText.gameObject.SetActive(true);
        }
        void BounceSumText()
        {
            DisableTexts();
            _sumDiceText.transform.DOScale(0.05f, .7f).SetEase(Ease.OutBack);
            _sumDiceText.transform.DOJump(_candyMachine.transform.position + Vector3.up * 2f, 1, 1, .7f).OnComplete(DropCandys);
        }
        public delegate void MyDelegate();
        public MyDelegate myDelegate;

        void DropCandys()
        {
            _candyMachine.DropCandy(_diceSum);
            _sumDiceText.transform.position = _sumTextStartPos;
            _sumDiceText.gameObject.SetActive(false);
            myDelegate();
        }
    }
}

