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
                var uc = Random.insideUnitCircle;

                Instantiate(_candyPrefab, _candySpawnPosT.position + new Vector3(uc.x, uc.y, 0), Quaternion.identity);
            }
            print(amount);
        }
    }
}

