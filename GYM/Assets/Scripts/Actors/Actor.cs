using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class Actor : MonoBehaviour
    {
        public int CurrentGymPoints { get; private set; }

        public void UpdateGymPoints(int amount)
        {
            CurrentGymPoints += amount;
            if (CurrentGymPoints < 0)
                CurrentGymPoints = 0;
        }
        public void SitOn(Vector3 position)
        {

        }
    }

}
