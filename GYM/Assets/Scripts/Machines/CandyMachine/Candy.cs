using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class Candy : MonoBehaviour
    {
        [SerializeField] TextMesh _textMesh;

        int _prizeAmount;

        public void SetPrizeAmount(int amount)
        {
            _prizeAmount = amount;

            UpdateText();
        }
        void UpdateText()
        {
            _textMesh.text = _prizeAmount.ToString();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Actor"))
            {
                var actor = other.GetComponent<Actor>();

                Destroy(gameObject);
                //actor
            }
        }
    }
}

