using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Input.Controller
{
    [DisallowMultipleComponent]
    public class Controller
    {
        string mInputName;
        float mInputValue;
        UnityAction<float> mInputInteraction;
        Dictionary<string, float> mInputAxisRawMap;
        List<IInputInteraction> mControllTargets = new List<IInputInteraction>();

        public void AddInputInteraction(IInputInteraction target)
        {
            mControllTargets.Add(target);
        }
        public void AddInputInteractions(List<IInputInteraction> targets)
        {
            mControllTargets.AddRange(targets);
        }
        public void RemoveInputInteractionTarget(IInputInteraction target)
        {
            mControllTargets.Remove(target);
        }
        public void RemoveInputInteractionTargets(List<IInputInteraction> targets)
        {
            foreach (var target in targets)
            {
                mControllTargets.Remove(target);
            }
        }
        public void ClearInputInteractionTargets()
        {
            mControllTargets.Clear();
        }
        public void Update()
        {
            if(mControllTargets.Count== 0)
            {
                return;
            }
            mInputAxisRawMap = ActionKey.GetAxisRawMap();
            foreach (var interactionTarget in mControllTargets.ToList())
            {
                if (!interactionTarget.IsAbleNow())
                {
                    continue;
                }

                foreach (var item in interactionTarget.InputEventMap)
                {
                    mInputName = item.Key;
                    mInputInteraction = item.Value;
                    mInputValue = mInputAxisRawMap[mInputName];

                    if(mInputValue != 0)
                    {
                        mInputInteraction.Invoke(mInputValue);
                    }
                }
            }
        }
    }
}
