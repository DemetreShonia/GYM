using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    
    public class SlotDiceMachine : Singleton<SlotDiceMachine>
    {
        [SerializeField] Dice[] dices;

        public List<int> diceScores = new List<int>();

        [SerializeField] int _diceAmount;

        Actor _currentActor;
        bool _isMachineAvailable = true;

        // jer ert kamatelze vqnat
        
        void Start()
        {
            RollDices();
        }

        public void SitActorOnMe(Actor _actor)
        {
            _currentActor = _actor;
            _isMachineAvailable = false;
        }
        public void StandDownFromSlotMachine()
        {
           // _currentActor.StandUp()
            _currentActor = null;
            _isMachineAvailable = true;
        }
        // Update is called once per frame
        void Update()
        {
            if (diceScores.Count == _diceAmount) // es 2 damokidebulia kamatlebis odenobaze
            {
                for (int i = 0; i < diceScores.Count; i++)
                {
                    print(diceScores[i]);
                }
                diceScores.Clear();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                RollDices();
            }
            //CheckScore()
            //CheckDicesScore();
        }
        void RollDices()
        {
            for (int i = 0; i < dices.Length; i++)
            {
                dices[i].RollDice();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Actor"))
            {
                var actor = other.GetComponent<Actor>();
                SitActorOnMe(actor);
                actor.SitOnSlotMachine();
            }
        }

    }

}
