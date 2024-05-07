using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame.Character.Animation
{
    [Serializable]
    public class StateTriggerPair
    {
        public CharacterState State;
        public string Trigger;
    }

    [RequireComponent(typeof(Character))]
    public class CharacterAnimation : MonoBehaviour
    {
        public Animator EditorAnimator => mAnimator == null ? null : mAnimator;
        [HideInInspector] public RuntimeAnimatorController BeforeController;
        [HideInInspector] public List<StateTriggerPair> EditorStateTriggers = new List<StateTriggerPair>();

        Character mCharacter;
        CharacterState mLastState;
        Dictionary<CharacterState, string> mStateMap;
        [SerializeField] Rigidbody mRigid;
        [SerializeField] Animator mAnimator;


        public void UpdateAnimation()
        {
            var state = mCharacter.State;
            if (mLastState == state)
            {
                return;
            }
            mLastState = state;
            if (mAnimator == null)
            {
                return;
            }
            foreach (var t in mStateMap.Values)
            {
                mAnimator.ResetTrigger(t);
            }
            var trigger = mStateMap[state];
            mAnimator.SetTrigger(trigger);
        }

        void Awake()
        {
            Debug.Assert(mRigid);
            Debug.Assert(mAnimator);
            mCharacter = GetComponent<Character>();
            mStateMap = new Dictionary<CharacterState, string>();
            foreach (var pair in EditorStateTriggers)
            {
                Debug.Assert(!mStateMap.ContainsKey(pair.State), $"{pair.State}");
                mStateMap.Add(pair.State, pair.Trigger);
            }
        }
        void Update()
        {
            UpdateAnimation();
            if (mLastState == CharacterState.Walk ||
                mLastState == CharacterState.Running)
            {
                LookAtDirection.LookAtMovingDirection(mAnimator.transform, mRigid);
            }
        }
    }
}