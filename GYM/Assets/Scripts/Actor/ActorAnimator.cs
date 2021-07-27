using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class ActorAnimator : MonoBehaviour
    {
        Animator _animator;
        ActorMovement _actorMovement;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _actorMovement = GetComponent<ActorMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }
        void UpdateAnimator()
        {
            _animator.SetBool("IsMoving", _actorMovement.isMoving);
        }
        public void TurnOnWorkOutPoseAnimation(int workOutId)
        {
            _animator.SetTrigger("SitOnMachine");
            _animator.SetInteger("WorkOutId", workOutId);
        }
    }
}
