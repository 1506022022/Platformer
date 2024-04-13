using RPG.Character;
using RPG.Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Character.Character))]
    public class Combat : MonoBehaviour, ITransitionAnimation, IInputInteraction
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
            mDelay = Time.time + 0.6f;
            mAnimator.SetTrigger(mTrigger);
        }
        public void Guard()
        {
            if (Time.time < mDelay)
            {
                return;
            }
            mTrigger = AnimationTrigger.GUARD;
            mDelay = Time.time + 1.0f;
            mAnimator.SetTrigger(mTrigger);
        }
        public State UpdateAndGetState()
        {
            if (mDelay <= Time.time)
            {
                mTrigger = AnimationTrigger.IDLE;
            }
            return AnimationTrigger.STATE_MAP[mTrigger];
        }

        // IInputInteraction
        public Dictionary<string, UnityAction<float>> InputEventMap
        {
            get;
            private set;
        }
        Character.Character mCharacter;
        public bool IsAble()
        {
            return mCharacter.State == State.Idle ||
                    mCharacter.State == State.Running;
        }
        void MappingInputEvent()
        {
            InputEventMap = new Dictionary<string, UnityAction<float>>()
            {
                { ActionKey.ATTACK,       (f) =>{ if(!f.Equals(0)) Attack(); } },
                { ActionKey.GUARD,        (f) =>{ if(!f.Equals(0)) Guard(); } }
            };
        }
        void Awake()
        {
            mCharacter = GetComponent<Character.Character>();
            MappingInputEvent();
        }
    }
}