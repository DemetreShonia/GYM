using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HappyBat
{
    public class ActorScoreManager : MonoBehaviour
    {
        SkinnedMeshRenderer _skinnedMeshRenderer;

        int _currentCandyAmount;
        [SerializeField] int _maxCandyAmount;
        [SerializeField] Slider _skinnyIndicatorSlider;
        [SerializeField] GameObject _maxText;
        [SerializeField] Transform _pivotTransform;
        float currentCandyPercent
        {
            get
            {
                if (_maxCandyAmount != 0)
                {
                    if (_currentCandyAmount == _maxCandyAmount)
                    {
                        _hasMaxCandy = true;
                        _maxText.SetActive(true);
                        return 200;

                    }
                    else
                    {
                        return (float)(_currentCandyAmount) / _maxCandyAmount * 200;
                    }
                }
                

                else
                    return 0;
            }
        }
        bool _hasMaxCandy;

        private void Start()
        {
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }
        private void Update()
        {
            if (!_hasMaxCandy)
            {
                UpdateBlendShapes();
                UpdateUI();
            }
            UpdatePlayerScale();
            
        }
        void UpdatePlayerScale()
        {
            _pivotTransform.localScale = Vector3.one + Vector3.one * (_currentCandyAmount / 10f);
        }
        void UpdateBlendShapes()
        {
            _skinnedMeshRenderer.SetBlendShapeWeight(0, currentCandyPercent);
        }
        void UpdateUI()
        {
            _skinnyIndicatorSlider.value = currentCandyPercent / 100f;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Candy"))
            {
                if (_currentCandyAmount < _maxCandyAmount)
                {
                    _currentCandyAmount++;
                    Destroy(collision.gameObject);
                }
            }
        }

    }
}

