using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public abstract class GymMachine : MonoBehaviour
    {
        public abstract void Update();

        [SerializeField] Transform _actorSitPosT;
        [SerializeField] Transform _actorStandPosT;
        [SerializeField] WorkOutType _workOutType;

        [HideInInspector] public GameObject uiInputGO;

        bool _amIAvailable = true;

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
        private void Start()
        {
            uiInputGO.SetActive(false); // chaqres UI
        }
        public void SitActorOnMe(Actor actor)
        {
            if(currentActor == null)
            {
                currentActor = actor;
                actor.SitOn(_actorSitPosT, workOutId);
                uiInputGO.GetComponent<GymUiBase>().Reset();
                IAmUnAvailable();
                uiInputGO.SetActive(true); // gamochndes UI
            }
            
        }
        
        public void StandActorFromMe()
        {
            if(currentActor != null)
            {
                currentActor.StandUp(_actorStandPosT.position);
                currentActor = null;
                uiInputGO.GetComponent<GymUiBase>().Reset();
                uiInputGO.SetActive(false); // chaqres UI
                Invoke("IAmAvailable", 1); // trigershi ro ar gaixlartos
            }
            
        }
        void IAmAvailable()
        {
            _amIAvailable = true;
        }
        void IAmUnAvailable()
        {
            _amIAvailable = false;
        }
        public virtual void Train(int reward)
        {
            currentActor.WorkOut(_workOutType, reward * _trainRewardMultiplier);
        }
        public virtual void OnTriggerEnter(Collider other)
        {
            if (_amIAvailable)
            {
                var actor = other.GetComponent<Actor>();
                if (actor != null)
                    SitActorOnMe(actor);
            }
            
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
