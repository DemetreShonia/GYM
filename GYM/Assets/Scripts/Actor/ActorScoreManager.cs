using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class ActorScoreManager : MonoBehaviour
    {
        SkinnedMeshRenderer _skinnedMeshRenderer;

        int _currentCandyAmount;
        [SerializeField] int _maxCandyAmount;
        float currentCandyPercent
        {
            get
            {
                if (_maxCandyAmount != 0)
                    return (float)(_currentCandyAmount) / _maxCandyAmount * 100;
                else
                    return 0;
            }
        }
        private void Start()
        {
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }
        private void Update()
        {
            UpdateBlendShapes();
            print(currentCandyPercent);
        }

        void UpdateBlendShapes()
        {
            _skinnedMeshRenderer.SetBlendShapeWeight(0, currentCandyPercent);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Candy"))
            {
                if(_currentCandyAmount < _maxCandyAmount)
                {
                    _currentCandyAmount++;
                    Destroy(other.gameObject);
                }
                
            }
        }
        
    }
}

