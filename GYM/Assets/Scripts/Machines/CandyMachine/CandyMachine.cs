using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class CandyMachine : Singleton<CandyMachine>
    {
        [SerializeField] GameObject _candyPrefab;
        [SerializeField] Transform _candySpawnPosT;
        [SerializeField] Transform _candyBounceSpawnPosT;
        [SerializeField] int _candyAmountToBounce;


        public void DropCandy(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject candy = Instantiate(_candyPrefab, _candySpawnPosT.position + Random.insideUnitSphere, Quaternion.identity);
                candy.transform.SetParent(transform.root);
                Destroy(candy, Random.Range(8, 15));
            }
        }
    }
}

