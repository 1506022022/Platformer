using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static RPG.Character.AnimationTrigger;

namespace RPG.Character
{
    public class Combat : ITransitionAnimation
    {
        float mDelay;
        string mTrigger;
        Rigidbody mRigid;
        Animator mAnimator;
        Dictionary<string, UnityAction<float>> mActionMap;

        public bool IsTransitionAbleState(State currentState)
        {
            return currentState == State.Idle ||
                   currentState == State.Running;
        }
        public void SetAnimationTarget(Animator animator, Rigidbody rigid)
        {
            mAnimator = animator;
            mRigid = rigid;
            mTrigger = IDLE;
        }
        public void Attack()
        {
            if (Time.time < mDelay)
            {
                return;
            }
            mTrigger = ATTACK;
            mDelay = Time.time + 0.6f;
            mAnimator.SetTrigger(mTrigger);
        }
        public void Guard()
        {
            if (Time.time < mDelay)
            {
                return;
            }
            mTrigger = GUARD;
            mDelay = Time.time + 1.0f;
            mAnimator.SetTrigger(mTrigger);
        }
        public State UpdateAndGetState()
        {
            if (mDelay <= Time.time)
            {
                mTrigger = IDLE;
            }
            return STATE_MAP[mTrigger];
        }
    }
}