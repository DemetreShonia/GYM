using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HappyBat
{
    public class InputManager : Singleton<InputManager>
    {
        #region Events
        [SerializeField] UnityEvent onTap;
        [SerializeField] UnityEvent onHold;
        [SerializeField] UnityEvent onSlide; //....
        #endregion

        #region Swipe
        Vector2 firstPressPos;
        Vector2 secondPressPos;
        Vector2 currentSwipe;
        #endregion

        #region HoldOrTap
        bool _isHeld = false;
        [SerializeField] float minHoldTime;
        float _holdTimer;
        #endregion

        void Start()
        {

        }

        void Update()
        {
            HoldOrTap();
        }
       
        
        void HoldOrTap()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isHeld = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isHeld = false;
            }
            if (_isHeld)
            {
                _holdTimer += Time.deltaTime;
            }
            else
            {
                if (_holdTimer > minHoldTime)
                {
                    print("Hold");
                }
                else
                {
                    print("Tap");
                }

                _holdTimer = 0f; //reset
            }
            

        }

        void Swipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            if (Input.GetMouseButtonUp(0))
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                currentSwipe.Normalize();

                if (currentSwipe.y > 0 &&  currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f)
                {
                    Debug.Log("up swipe");
                }
                if (currentSwipe.y < 0 &&  currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("down swipe");
                }
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("left swipe");
                }
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("right swipe");
                }
            }
        }
    }
}
