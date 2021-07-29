using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class Press : GymMachine
    {
        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                StopWorkout();
            

        }
        
    }
}

