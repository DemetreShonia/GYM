using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        [SerializeField] float _moveSpeed;
        [SerializeField] Transform _targetT;

        Vector3 _offset;

        private void Start()
        {
            _offset = _targetT.position - transform.position;
        }
        

        private void Update()
        {
            if (_targetT != null)
            {
                transform.position = Vector3.Lerp(transform.position, _targetT.position - _offset, _moveSpeed * Time.deltaTime);
            }
        }
    }
}

