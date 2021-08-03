using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class CandyMachine : MonoBehaviour
    {
        [SerializeField] GameObject _candyPrefab;
        [SerializeField] Transform _candySpawnPosT;

        int _candyAmountToDrop;

        public void DropCandy()
        {
            var candyGo = Instantiate(_candyPrefab, _candySpawnPosT.position, Quaternion.identity);
            var candy = candyGo.GetComponent<Candy>();

            candy.SetPrizeAmount(_candyAmountToDrop);
        }
        public void SetCandyAmountToDrop(int amount)
        {
            _candyAmountToDrop = amount;
        }
    }
}

