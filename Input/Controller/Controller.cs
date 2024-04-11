using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Input.Controller
{
    public class Controller
    {
        public bool IsActive => mbActive;
        bool mbActive;
        IControllableObject mControlledTarget;
        public List<IControllableObject> AllControlledTargets
        {
            get;
            private set;
        }
        List<IInputInteraction> mInputInteractionList = new List<IInputInteraction>();
        string mKeyName;
        float mKeyInputValue;
        XZMovement mXZMovement;
        JumpMovement mJumpMovement;
        UnityAction<float> mInputEvent;
        List<IControllableObject> mReleasedTarget = new List<IControllableObject>();
        public Controller()
        {
            mXZMovement = new XZMovement();
            mInputInteractionList.Add(mXZMovement);
            mJumpMovement = new JumpMovement();
            mInputInteractionList.Add(mJumpMovement);
            var swappingSystem = new CharacterSwappingSystem(this);
            mInputInteractionList.Add(swappingSystem);
        }

        public virtual void Update()
        {
            if (!IsActive) return;

            Dictionary<string, float> axisRawMap = ActionKey.GetAxisRawMap();
            foreach (var interactionTarget in mInputInteractionList)
            {
                if (!interactionTarget.IsAble)
                {
                    continue;
                }

                foreach (var item in interactionTarget.InputEventMap)
                {
                    mKeyName = item.Key;
                    mInputEvent = item.Value;
                    mKeyInputValue = axisRawMap[mKeyName];

                    mInputEvent.Invoke(mKeyInputValue);
                }
            }
        }
        public void SetActive(bool active)
        {
            Debug.Assert(!active || active && mReleasedTarget.Count == 0);
            mbActive = active;
        }
        public virtual void BindObject(IControllableObject target)
        {
            mControlledTarget = target;
            mXZMovement.SetMovementObject(target);
            mJumpMovement.SetMovementObject(target);
            target.OnBindControll();
            mReleasedTarget.Clear();
        }
        public virtual void ReleaseObject()
        {
            mControlledTarget.OnReleaseControll();
            mReleasedTarget.Add(mControlledTarget);
            SetActive(false);
        }
        public void SetAllControlledTarget(List<IControllableObject> targets)
        {
            AllControlledTargets = targets;
        }
    }
}
