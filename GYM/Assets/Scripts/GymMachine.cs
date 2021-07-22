using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public abstract class GymMachine : MonoBehaviour
    {
        public abstract void Update();
        Actor _currentActor;

        public void SitActorOnMe(Actor actor)
        {
            _currentActor = actor;
        }
    }

}
