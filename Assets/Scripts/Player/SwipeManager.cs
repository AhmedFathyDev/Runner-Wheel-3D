using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static bool Tap, SwipeLeft, SwipeRight, SwipeUp, SwipeDown;

    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    // Update is called once per frame
    private void Update()
    {
        Tap = SwipeDown = SwipeUp = SwipeLeft = SwipeRight = false;

        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0))
        {
            Tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }

        #endregion

        #region Mobile Input

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Tap = true;
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

        //Calculate the distance

        swipeDelta = Vector2.zero;

        if (isDraging)
        {
            if (Input.touches.Length < 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        //Did we cross the distance?
        if (swipeDelta.magnitude > 100)
        {
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                {
                    SwipeLeft = true;
                }
                else
                { 
                    SwipeRight = true; 
                }
            }
            else
            {
                //Up or Down
                if (y < 0)
                {
                    SwipeDown = true;
                }
                else
                {
                    SwipeUp = true;
                }
            }

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
