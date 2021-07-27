using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public abstract class GymMachine : MonoBehaviour
    {
        public abstract void Update();

        [SerializeField] Transform _actorSitPosT;
        [SerializeField] WorkOutType _workOutType;

        public int workOutId
        {
            get
            {
                return (int)_workOutType;
            }
        }

        public Actor currentActor;
        [HideInInspector] public float lastClickTime;

        [SerializeField] int _trainRewardMultiplier;

        public void SitActorOnMe(Actor actor)
        {
            currentActor = actor;
            actor.SitOn(_actorSitPosT.position, workOutId);
            print(workOutId); ;
        }
        public virtual void Train(int reward)
        {
            currentActor.WorkOut(_workOutType, reward * _trainRewardMultiplier);
        }
        public virtual void OnTriggerEnter(Collider other)
        {
            var actor = other.GetComponent<Actor>();
            if (actor != null)
                SitActorOnMe(actor);
        }

        //public virtual int GiveMeRewardAmount()
        //{
        //    var timePassedSinceLastClick = Time.time - lastClickTime;

        //    float newTrainReward = trainReward / timePassedSinceLastClick;

        //    lastClickTime = Time.time;

        //    return Mathf.CeilToInt(newTrainReward);

        //}
    }

}
