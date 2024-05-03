using Platformer;
using RPG.Character;
using RPG.Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG
{
    public class AttackAbility : Ability
    {
        // ITransitionAnimation
        float mDelay;
        string mTrigger;
        [SerializeField] Animator mAnimator;

        public bool IsTransitionAbleState(State currentState)
        {
            return currentState == State.Idle ||
                   currentState == State.Running ||
                   currentState == State.Attack;
        }
        public void Attack()
        {
            if (Time.time < mDelay)
            {
                return;
            }
            mTrigger = AnimationTrigger.ATTACK;
            mAnimator.SetTrigger(mTrigger);
            mDelay = Time.time + 0.533f;

        }
        //public State UpdateAndGetState()
        //{
        //    if (mDelay <= Time.time)
        //    {
        //        mTrigger = AnimationTrigger.IDLE;
        //    }
        //    return AnimationTrigger.STATE_MAP[mTrigger];
        //}

        // IInputInteraction
        protected override void MappingInputEvent()
        {
            InputEventMap = new Dictionary<string, UnityAction<float>>()
            {
                { ActionKey.ATTACK,       (f) => Attack()  }
            };
        }

        protected override void UpdateAbilityState()
        {
            if (Time.time < mDelay)
            {
                //mAbilityState = AbilityState.Action;
            }
            else
            {
                //mAbilityState = AbilityState.Ready;
            }
        }
    }
}