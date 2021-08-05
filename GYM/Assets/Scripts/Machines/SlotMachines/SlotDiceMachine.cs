using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HappyBat
{
    
    public class SlotDiceMachine : Singleton<SlotDiceMachine>
    {
        [SerializeField] Dice[] dices;

        public List<int> diceScores = new List<int>();

        [SerializeField] int _diceAmount;

        [SerializeField] Button _stopWorkOutButton;

        [SerializeField] Transform _standPosT;
        [SerializeField] Transform _standUpPosT;

        [SerializeField] DiceTexts _diceTexts;
        [SerializeField] Transform _triggerTransform;

        Actor _currentActor;

        // jer ert kamatelze vqnat
        
        void Start()
        {
            //RollDices();
        }

        
        public void SitActorOnMe(Actor _actor)
        {
            if (_currentActor == null)
            {
                _currentActor = _actor;

                //  actor.SitOn(_actorSitPosT, workOutId);

                if (_currentActor.isPlayer)
                {
                    //uiInputGO.SetActive(true); // gamochndes UI
                    //uiInputGO.GetComponent<GymUiBase>().SitOn();
                    _currentActor.transform.position = _standPosT.position;
                    _currentActor.transform.rotation = _standPosT.rotation;

                   // _stopWorkOutButton.gameObject.SetActive(true);
                   // _stopWorkOutButton.onClick.AddListener(StandUpFromSlotMachine);
                }
                else
                {
                    var __actorAi = _currentActor.GetComponent<ActorAI>();
                    //StartCoroutine(__actorAi.StartWorkOutCo(_workOutType, 0.5f));
                }
            }

        }
        public void StandUpFromSlotMachine()
        {
            // _currentActor.StandUp()

            if (_currentActor != null)
            {

                if (_currentActor.isPlayer)
                {
                    //     uiInputGO.GetComponent<GymUiBase>().Reset();
                   // uiInputGO.SetActive(false); // chaqres UI

                  //  _stopWorkOutButton.onClick.RemoveListener(StandUpFromSlotMachine);
                 //   _stopWorkOutButton.gameObject.SetActive(false);

                }
                else
                {
                    _currentActor.GetComponent<ActorAI>().StopAIWorkOut();
                }

                //_currentActor.StandUp(_standUpPosT.position);

                _currentActor = null;

               // Invoke("IAmAvailable", 1); // trigershi ro ar gaixlartos
            }
        }
        // Update is called once per frame

        bool _areDicesRolling = false;
        void Update()
        {
            if (diceScores.Count == _diceAmount) // es 2 damokidebulia kamatlebis odenobaze
            {
                //for (int i = 0; i < diceScores.Count; i++)
                //{
                //    print(diceScores[i]);
                //}
                ResetStandTrigger();
                _diceTexts.ShowText(dices[0].currentScore, dices[1].currentScore);
                CandyMachine.instance.DropCandy(CountScore());
                _areDicesRolling = false;
                diceScores.Clear();
            }
            
        }
        int CountScore()
        {
            var sum = 0;

            for (int i = 0; i < diceScores.Count; i++)
            {
                sum += diceScores[i];
            }
            return sum;
        }
        void RollDices()
        {
            _areDicesRolling = true;

            for (int i = 0; i < dices.Length; i++)
            {
                dices[i].RollDice();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Actor"))
            {
                // var actor = other.GetComponent<Actor>();
                // SitActorOnMe(actor);
                // actor.SitOnSlotMachine();

                if (!_areDicesRolling)
                {
                    StandOnTrigger();
                    RollDices();
                }
            }
        }
        void StandOnTrigger()
        {
            var scale = _triggerTransform.localScale;
            _triggerTransform.localScale = new Vector3(scale.x, 0.1f, scale.z);
        }
        void ResetStandTrigger()
        {
            var scale = _triggerTransform.localScale;
            _triggerTransform.localScale = new Vector3(scale.x, 0.2f, scale.z);
        }
    }

}
