using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class TreadMill : GymMachine
    {
        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Train(GiveMeRewardAmount());
            }
        }
    }
}

