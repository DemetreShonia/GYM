using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    
    public class SlotDiceMachine : Singleton<SlotDiceMachine>
    {
        [SerializeField] Dice[] dices;

        public List<int> diceScores = new List<int>();
        
        // jer ert kamatelze vqnat
        
        void Start()
        {
            RollDices();
        }

        // Update is called once per frame
        void Update()
        {
            if (diceScores.Count == 2) // es 2 damokidebulia kamatlebis odenobaze
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
        
    }

}
