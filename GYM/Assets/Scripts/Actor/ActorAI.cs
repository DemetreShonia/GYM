using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class ActorAI : MonoBehaviour
    {
        Actor _actor;
        
        //public IEnumerator StartWorkOutCo(WorkOutType workOutType, float delayBetweenAutoWorkOut)
        //{

        //    while (true)
        //    {
        //        yield return new WaitForSeconds(delayBetweenAutoWorkOut);
        //        _actor.WorkOut(workOutType, 10); // 10 for test
        //    }
        //}
        public void StopAIWorkOut()
        {
            StopAllCoroutines();
        }
        void Start()
        {
            _actor = GetComponent<Actor>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
