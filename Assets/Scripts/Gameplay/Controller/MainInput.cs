using UnityEngine;

namespace TraineeGame
{
    public class MainInput : MonoBehaviour, IMovement
    {
        /*
        private Vector2 _startTouchPosition;
        private Vector3 endTouchPosition;

        private Vector2 _swipeDelta;

        private bool swipeUp;
        private bool swipeDown;
        private bool swipeLeft;
        private bool swipeRight;

        private bool _isSwiping;
        private float _deadZone = 50;

        void Update()
        {
            Controller();
        }

        public void Controller()
        {
            Debug.Log("fsfsdfsf");

            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _isSwiping = true;
                    _startTouchPosition = Input.GetTouch(0).position;
                }

                else if (Input.GetTouch(0).phase == TouchPhase.Canceled ||
                    Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    DetectSwipe();
                }

            }
        }

        private void ResetSwipe()
        {
            _isSwiping = false;
            _startTouchPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;
            swipeLeft = false;
            swipeRight = false;
            swipeUp = false;
            swipeDown = false;
        }

        /*
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    endTouchPosition = touch.position;
                }
            }

            DetectSwipe();*/

        /*
            float swipeDistanceThreshold = 50f;

            Vector2 swipeDirection = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(swipeDirection.x) > swipeDistanceThreshold ||
                Mathf.Abs(swipeDirection.y) > swipeDistanceThreshold)
            {
                if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                {


                    if (swipeDirection.x > 0)
                    {
                        swipeRight = true;
                    }
                    else
                    {
                        swipeLeft = true;
                    }
                }
                else
                {
                    if (swipeDirection.y > 0)
                    {
                        swipeUp = true;
                    }
                    else
                    {
                        swipeDown = true;
                    }
                }
            }*/
        /*
        private void DetectSwipe()
        {
            _swipeDelta = Vector2.zero;
            if (_isSwiping)
            {
                if(Input.touchCount > 0)
                {
                    _swipeDelta = Input.GetTouch(0).position - _startTouchPosition;
                }
            }

            if(_swipeDelta.magnitude > _deadZone)
            {
                if(Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                {
                    if (_swipeDelta.x > 0)
                    {
                        Debug.Log("Right");
                        swipeRight = true;
                    }


                    else
                    {
                        Debug.Log("Left");
                        swipeLeft = true;
                    }

                    }

                else
                {
                    if(_swipeDelta.y > 0)
                    {
                        Debug.Log("Up");
                        swipeUp = true;
                    }

                    else
                    {
                        Debug.Log("Down");
                        swipeDown = true;
                    }
                }

                ResetSwipe();
            }
        }
        */

        private Vector2 _startPos = Vector2.zero;

        private bool _swipeLeft;
        private bool _swipeRight;
        private bool _swipeUp;
        private bool _swipeDown;
        private bool _swipeRegistered;

        public void ResetLeft()
        {
            _swipeLeft = false;
        }

        public void ResetRight()
        {
            _swipeRight = false;
        }

        public bool GoLeft()
        {
            return _swipeLeft;
        }

        public bool GoRight()
        {
            return _swipeRight;
        }
        public bool GoUp()
        {
            return _swipeUp;
        }
        public bool GoDown()
        {
            return _swipeDown;
        }

        void Update()
        {
            Controller();
        }

        public void Controller()
        {
            _swipeLeft = _swipeRight = _swipeUp = _swipeDown = false;

            if (Time.timeScale == 0f)
                return;

            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startPos = touch.position;
                    _swipeRegistered = false;
                    break;

                case TouchPhase.Moved:
                    if (_swipeRegistered)
                        return;
                    Vector2 deltaSwipe = touch.position - _startPos;
                    if (Mathf.Abs(deltaSwipe.x) > Mathf.Abs(deltaSwipe.y))
                    {
                        _swipeLeft |= deltaSwipe.x < 0;
                        _swipeRight |= deltaSwipe.x > 0;
                    }
                    else
                    {
                        _swipeUp |= deltaSwipe.y > 0;
                        _swipeDown |= deltaSwipe.y < 0;
                    }
                    _swipeRegistered = true;
                    break;

                case TouchPhase.Ended:
                    _swipeRegistered = false;
                    break;

            }

        }

        /*
        public bool GoUp()
        {
            return swipeUp;
        }

        public bool GoDown()
        {
            return swipeDown;
        }

        public bool GoRight()
        {
            return swipeRight;
        }

        public bool GoLeft()
        {
            return swipeLeft;
        }*/
    }
}
