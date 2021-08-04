using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class Dice : MonoBehaviour
    {
        [SerializeField] float _kickForce;
        
        Rigidbody _diceRb;

        enum SidePrizeAmount
        {
            Up = 2,
            Down = 5,
            Left = 4,
            Right = 3,
            Forward = 1,
            Back = 6
        }

        // Start is called before the first frame update
        void Awake()
        {
            _diceRb = GetComponent<Rigidbody>();
        }
        public int currentScore = -1;
        public bool isRolling { get; private set; }

        // Update is called once per frame
        IEnumerator RollDiceCo()
        {
            yield return new WaitForSeconds(0.5f);

            while (isRolling)
            {
                if (_diceRb.velocity.sqrMagnitude < 0.001f)
                {
                    _diceRb.velocity = Vector3.zero;

                    var score = ReturnScore();

                    if (score > 0)
                    {
                        currentScore = score;
                        isRolling = false;
                        SlotDiceMachine.instance.diceScores.Add(currentScore);
                        break;
                    }
                }
                yield return new WaitForFixedUpdate();
            }
        }
        public void RollDice()
        {
            //_diceRb.AddForce((Random.insideUnitSphere.normalized + Vector3.up) * _kickForce, ForceMode.Impulse);
            //_diceRb.AddTorque((Random.insideUnitSphere.normalized + Vector3.up) * _kickForce, ForceMode.Impulse);
            
            _diceRb.AddForce(Vector3.up * Random.Range(0.8f, 1.2f) * _kickForce, ForceMode.Impulse);

            _diceRb.AddTorque((Random.insideUnitSphere) * _kickForce, ForceMode.Impulse);

            isRolling = true;
            StartCoroutine(RollDiceCo());
        }
        int ReturnScore()
        {
            if (Vector3.Dot(transform.forward, Vector3.up) >= 1)
            {
                return (int)SidePrizeAmount.Forward;
            }
            else if(Vector3.Dot(-transform.forward, Vector3.up) >= .9f)
            {
                return ((int)SidePrizeAmount.Back);
            }
            else if (Vector3.Dot(transform.up, Vector3.up) >= .9f)
            {
                return  ((int)SidePrizeAmount.Up);
            }
            else if (Vector3.Dot(-transform.up, Vector3.up) >= .9f)
            {
                return  ((int)SidePrizeAmount.Down);
            }
            else if (Vector3.Dot(transform.right, Vector3.up) >= .9f)
            {
                return  ((int)SidePrizeAmount.Right);
            }
            else if (Vector3.Dot(-transform.right, Vector3.up) >= .9f)
            {
                return  ((int)SidePrizeAmount.Left);
            }
            return -1;
        }
    }

}
