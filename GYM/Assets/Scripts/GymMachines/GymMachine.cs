using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HappyBat
{
    public abstract class GymMachine : MonoBehaviour
    {
        public abstract void Update();

        [Header("References")]
        [SerializeField] Transform _actorSitPosT;
        [SerializeField] Transform _actorStandPosT;
        [SerializeField] Button _stopWorkOutButton;
        [SerializeField] GameObject _finishedText;
        [HideInInspector] public GameObject uiInputGO;

        [Header("Values")]
        [SerializeField] WorkOutType _workOutType;
        [SerializeField] int _trainRewardMultiplier;

        public bool amIAvailable { get; private set; }

        public int workOutId
        {
            get
            {
                return (int)_workOutType;
            }
        }

        public Actor currentActor;
        [HideInInspector] public float lastClickTime;

        private void Start()
        {
            uiInputGO.SetActive(false); // chaqres UI
            amIAvailable = true;
        }
        public void SitActorOnMe(Actor actor)
        {
            if (!actor.workOutData.HasFinished(_workOutType))
            {
                if (currentActor == null)
                {
                    currentActor = actor;
                    actor.SitOn(_actorSitPosT, workOutId);

                    if (currentActor.isPlayer)
                    {
                        uiInputGO.SetActive(true); // gamochndes UI
                        uiInputGO.GetComponent<GymUiBase>().SitOn();
                        _stopWorkOutButton.gameObject.SetActive(true);
                        _stopWorkOutButton.onClick.AddListener(StopWorkout);
                    }
                    else
                    {
                        var __actorAi = currentActor.GetComponent<ActorAI>();
                        StartCoroutine(__actorAi.StartWorkOutCo(_workOutType, 0.5f));
                    }
                    
                    
                    IAmUnAvailable();

                    
                }
            }
           
            
            
        }
        
        public void StopWorkout()
        {
            if(currentActor != null)
            {
                
                if (currentActor.isPlayer)
                {
                    //     uiInputGO.GetComponent<GymUiBase>().Reset();
                    uiInputGO.SetActive(false); // chaqres UI

                    _stopWorkOutButton.onClick.RemoveListener(StopWorkout);
                    _stopWorkOutButton.gameObject.SetActive(false);
                }
                else
                {
                    currentActor.GetComponent<ActorAI>().StopAIWorkOut();
                }

                currentActor.StandUp(_actorStandPosT.position);
                currentActor = null;
                
                Invoke("IAmAvailable", 1); // trigershi ro ar gaixlartos
            }
            
        }
        void IAmAvailable()
        {
            amIAvailable = true;
        }
        void IAmUnAvailable()
        {
            amIAvailable = false;
        }
        public virtual void Train(int reward)
        {
            if(currentActor != null)
            {
                if (currentActor.workOutData.HasFinished(_workOutType))
                {
                    StopWorkout();
                    _finishedText.SetActive(true); // NOTE, THIS FINISH TEXT IS SHOWN TO ALL PLAYERS // WORKS FOR BOTS ONLY
                }
                else
                {
                    currentActor.WorkOut(_workOutType, reward * _trainRewardMultiplier);
                }
            }

        }
        public virtual void OnTriggerEnter(Collider other)
        {
            if (amIAvailable)
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
