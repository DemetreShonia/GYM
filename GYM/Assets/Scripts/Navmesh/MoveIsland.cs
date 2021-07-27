using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HappyBat
{
    public class MoveIsland : MonoBehaviour
    {
        // ERTI KUNDZULI MODZRAOBS MXOLOD, MEORE DGAS !

        [Header("References")]
        [SerializeField] NavMeshAgent[] _myNavMeshAgents;

        [Header("Values")]
        [SerializeField] float _moveSpeed;
        Vector3 _moveDir = Vector3.back;

        bool _shouldMove = true;
       
        void Update()
        {
            if (!_shouldMove)
                return;
            MoveThisIsland();
            MoveMyNavmeshAgents();
        }
        void MoveThisIsland()
        {
            transform.Translate(_moveDir * _moveSpeed * Time.deltaTime);
        }
        void MoveMyNavmeshAgents()
        {
            for (int i = 0; i < _myNavMeshAgents.Length; i++)
            {
                _myNavMeshAgents[i].Move(_moveDir * _moveSpeed * Time.deltaTime);
            }
        }
        public void StopMoving()
        {
            _shouldMove = false;
        }
        
    }

}
