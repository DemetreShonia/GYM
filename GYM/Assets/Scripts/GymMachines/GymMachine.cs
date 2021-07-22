using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public abstract class GymMachine : MonoBehaviour
    {
        public abstract void Update();

        [SerializeField] Transform actorSitPosT;

        public Actor currentActor;
        [HideInInspector] public float lastClickTime;

        public int trainReward;


        public void SitActorOnMe(Actor actor)
        {
            currentActor = actor;
            actor.SitOn(actorSitPosT.position);
        }
        public virtual void Train(int reward)
        {
            currentActor.UpdateGymPoints(reward);
        }
        public virtual int GiveMeRewardAmount()
        {
            var timePassedSinceLastClick = Time.time - lastClickTime;

            float newTrainReward = trainReward / timePassedSinceLastClick;

            lastClickTime = Time.time;

            return Mathf.CeilToInt(newTrainReward);

        }
    }

}
