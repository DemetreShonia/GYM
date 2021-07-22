using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HappyBat
{
    public class GymSlider : MonoBehaviour
    {
        public float maxValue = 5000f;
        public float decreaseAmount = 0.5f;
        float _currentAmount;
        public Actor actor;

        public Image image;
        

        // Update is called once per frame
        void Update()
        {
            var actorPoints = actor.CurrentGymPoints;

            if (actorPoints > (int)maxValue)
                actorPoints = (int)maxValue;

            image.fillAmount = 1 - (actorPoints / maxValue);

            actor.UpdateGymPoints(-Mathf.CeilToInt(decreaseAmount *Time.deltaTime));
        }
    }

}
