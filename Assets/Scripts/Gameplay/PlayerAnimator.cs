using UnityEngine;
using System;
using System.Collections.Generic;

namespace TraineeGame
{
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Slide,
        Die
    }

    public class PlayerAnimator : MonoBehaviour
    {
        private RuntimeAnimatorController _runtimeAnimatorController;
        private Dictionary<string, float> _animationlength;
        private Animator animator;
        private float _durationTime = 0.5f;

        public static PlayerState _playerState;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            _runtimeAnimatorController = animator.runtimeAnimatorController;
            InitAnimLength();

            GameManager.onEndGame += DeathAnimation;
            GameManager.onPreGame += StayAnimation;
            GameManager.onGameplay += RunAnimation;
        }



        private void InitAnimLength()
        {
            _animationlength = new Dictionary<string, float>();
            for(int i = 0; i < _runtimeAnimatorController.animationClips.Length; ++i)
            {
                _animationlength[_runtimeAnimatorController.animationClips[i].name] =
                    _runtimeAnimatorController.animationClips[i].length;
            }
        }

        public void SetAnimation(string animationName)
        {
            animator.CrossFade(animationName, _durationTime);
        }

        public float GetAnimationDuration(string animationName)
        {
            return _animationlength[animationName];
        }

        private void StayAnimation()
        {
            animator.CrossFade("Dwarf Idle", _durationTime);
        }

        private void RunAnimation()
        {
            animator.CrossFade("Fast Run", _durationTime);
        }

        private void DeathAnimation()
        {
            animator.CrossFade("Death", _durationTime);
        }

        private void OnDestroy()
        {
            GameManager.onEndGame -= StayAnimation;
            GameManager.onGameplay -= RunAnimation;
        }
    }
}