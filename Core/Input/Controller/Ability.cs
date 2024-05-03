using Platformer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Input
{
    public abstract class Ability : MonoBehaviour, IInputInteraction
    {
        // Ability
        public Func<bool> PublicCondition;
        Func<bool> mPrivateCondition;
        public AbilityState AbilityState
        {
            get
            {
                UpdateAbilityState();
                return mAbilityState;
            }
        }
        [ReadOnly(true)]
        [SerializeField]
        protected AbilityState mAbilityState;

        // IInputInteraction
        public Dictionary<string, UnityAction<float>> InputEventMap
        {
            get;
            protected set;
        }
        public bool IsAbleNow()
        {
            return (PublicCondition?.Invoke() ?? true) &&
                   (mPrivateCondition?.Invoke() ?? true);
        }
        protected abstract void UpdateAbilityState();
        protected abstract void MappingInputEvent();
        protected virtual void Awake()
        {
            mPrivateCondition = () => AbilityState == AbilityState.Ready;
            MappingInputEvent();
        }
    }
}

