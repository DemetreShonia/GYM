using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Transform _targetT;

        
        Vector3 _offset;
        private void Start()
        {
            _offset = transform.position - _targetT.position;
        }
        void LateUpdate()
        {
            transform.position = _targetT.position + _offset;
        }
    }
}

