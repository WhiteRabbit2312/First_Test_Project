using System.Collections;
using UnityEngine;
using System;

namespace TraineeGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;

        private IMovement _input;
        private float _centerPosX = 0f;
        private float _leftPosX = -1.4f;
        private float _rightPosX = 1.4f;

        private PlayerPos _playerPos;
        private bool _canControl = false;
        [SerializeField] private PlayerAnimator _playerAnimator;
        private Coroutine _jumpAnimation;
        private Coroutine _slideAnimation;
       
        private void Awake()
        {
#if UNITY_EDITOR

            _input = GetComponent<TestingInput>();

#elif UNITY_ANDROID
        _input = GetComponent<MainInput>();
#endif
            _playerPos = PlayerPos.Center;

            GameManager.onPreGame += Idle;
            GameManager.onPreGame += StopControl;
            GameManager.onGameplay += CanControl;
            GameManager.onEndGame += StopControl;
        }

        void Update()
        {
            Debug.Log("State: " + PlayerAnimator._playerState);

            if (_canControl)
            {
                if (_input.GoLeft())
                {
                    Debug.Log("Left");
                    Left();
                }

                if (_input.GoRight())
                {
                    Debug.Log("Right");
                    Right();
                }

                if (_input.GoUp())
                {
                    Debug.Log("Up. State: " + PlayerAnimator._playerState);
                    if(PlayerAnimator._playerState != PlayerState.Jump)
                        Jump();
                }

                if (_input.GoDown())
                {
                    Debug.Log("Down. State: " + PlayerAnimator._playerState);
                    if (PlayerAnimator._playerState != PlayerState.Slide)
                        Slide();
                }
            }
        }

        private void CanControl() => _canControl = true;
        private void StopControl() => _canControl = false;

        private void Idle()
        {
            transform.position = Vector3.zero;
            _playerPos = PlayerPos.Center;
        }

        private void Jump()
        {
            if(_slideAnimation != null)
            {
                StopCoroutine(_slideAnimation);
            }
            
            _jumpAnimation = StartCoroutine(PlayJumpAnimation());


        }

        private IEnumerator PlayJumpAnimation()
        {
            PlayerAnimator._playerState = PlayerState.Jump;
            _playerAnimator.SetAnimation("Big Jump");
            float duration = _playerAnimator.GetAnimationDuration("Big Jump");
            Debug.Log("Duration jump " + duration);
            yield return new WaitForSeconds(duration / 2);
            PlayerAnimator._playerState = PlayerState.Run;
            _playerAnimator.SetAnimation("Fast Run");
        }

        private void Slide()
        {
            if (_jumpAnimation != null)
            {
                Debug.Log("_slideAniamtion is null");
                StopCoroutine(_jumpAnimation);
                
            }
            _slideAnimation = StartCoroutine(PlaySlideAnimation());

            if (_slideAnimation == null)
            {
                Debug.Log("_jumpAnimation is null");
            }
        }

        private IEnumerator PlaySlideAnimation()
        {
            PlayerAnimator._playerState = PlayerState.Slide;
            _playerAnimator.SetAnimation("Running Slide");
            float duration = _playerAnimator.GetAnimationDuration("Running Slide");
            Debug.Log("Duration " + duration);
            yield return new WaitForSeconds((float)(duration / 1.5f));
            PlayerAnimator._playerState = PlayerState.Run;
            _playerAnimator.SetAnimation("Fast Run");
        }

        /*
        private bool CheckGround()
        {
            bool isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);
            Debug.Log("isGrounded " + isGrounded);

            return isGrounded;
        }

        private IEnumerator ToSlideMove(float newY = 0)
        {
            float t = 0f;
            _rb.velocity = Vector3.zero;
            Vector3 startPos = transform.position;
            while (transform.position.y != newY)
            {
                t += Time.deltaTime * 50f;
                transform.position = Vector3.Lerp(startPos,
                    new Vector3(transform.position.x, newY, transform.position.z), t);
                yield return new WaitForEndOfFrame();
            }

        }

        private IEnumerator ChangeColliderSize()// TODO
        {

            _collider.height = 0.9f;
            _collider.center = new Vector3(0f, 0.35f, 0f);

            yield return new WaitForSeconds(1f);
            _collider.height = 2f;
            _collider.center = new Vector3(0f, 1f, 0f);
        }
        */
        private void Left()
        {
            Debug.Log("Is left");

            StopCoroutine(ChangePosition());

            if (_playerPos == PlayerPos.Center)
            {
                StartCoroutine(ChangePosition(_leftPosX));

                _playerPos = PlayerPos.Left;
            }

            else if (_playerPos == PlayerPos.Right)
            {
                StartCoroutine(ChangePosition(_centerPosX));
                _playerPos = PlayerPos.Center;
            }
        }

        private IEnumerator ChangePosition(float newX = 0)
        {
            float t = 0f;
            Vector3 startPos = transform.position;
            while (transform.position.x != newX)
            {
                t += Time.deltaTime * 10f;
                transform.position = Vector3.Lerp(startPos,
                    new Vector3(newX, transform.position.y, transform.position.z), t);
                yield return new WaitForEndOfFrame();
            }

        }

        private void Right()
        {
            StopCoroutine(ChangePosition());
            if (_playerPos == PlayerPos.Center)
            {

                StartCoroutine(ChangePosition(_rightPosX));

                _playerPos = PlayerPos.Right;
            }

            else if (_playerPos == PlayerPos.Left)
            {
                StartCoroutine(ChangePosition(_centerPosX));

                _playerPos = PlayerPos.Center;
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "GateObstacle" && PlayerAnimator._playerState == PlayerState.Run)
            {
                Death();
            }

            if (other.gameObject.tag == "StoneObstacle")
            {
                if(PlayerAnimator._playerState == PlayerState.Run 
                    || PlayerAnimator._playerState == PlayerState.Slide)
                {
                    Death();
                }
                
            }
        }

        private void Death()
        {
            PlayerAnimator._playerState = PlayerState.Die;
            GameManager.GameOver();
        }

        private void OnDestroy()
        {
            GameManager.onPreGame -= Idle;
            GameManager.onPreGame -= StopControl;
            GameManager.onGameplay -= CanControl;
            GameManager.onEndGame -= StopControl;
        }
    }
}