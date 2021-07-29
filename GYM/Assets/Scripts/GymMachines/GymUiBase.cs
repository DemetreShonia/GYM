using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public abstract class GymUiBase : MonoBehaviour
    {
        public GymMachine _gymMachine;
        public int rewardAmount = 5;
        public abstract void Reset();
    }
        
}

