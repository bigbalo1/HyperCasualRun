using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiptManager : MonoBehaviour
{
    public static bool tap, swipeLeft, swipeRight, swipeDown, swipeUp;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelte;

    private void Update()
    {
        tap = swipeDown = swipeUp = swipeRight = swipeLeft = false;
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0) )
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if(Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        //calculate the distance
        swipeDelte = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length < 0)
                swipeDelte = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelte = (Vector2)Input.mousePosition - startTouch;
        }

        // did we cross the distance?
        if(swipeDelte.magnitude > 125)
        {
            //which direction
            float x = swipeDelte.x;
            float y = swipeDelte.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                //left or right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //up or down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelte = Vector2.zero;
        isDraging = false;
    }
}
