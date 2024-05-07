using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace PlatformGame.Input
{
    public class InputEvent
    {
        string mInputName;
        float mInputValue;
        UnityAction<float> mInputInteraction;
        Dictionary<string, float> mInputAxisRawMap;
        List<IInputEvent> mControllTargets = new List<IInputEvent>();

        public void AddInputInteraction(IInputEvent target)
        {
            mControllTargets.Add(target);
        }

        public void AddInputInteractions(List<IInputEvent> targets)
        {
            mControllTargets.AddRange(targets);
        }

        public void RemoveInputInteractionTarget(IInputEvent target)
        {
            mControllTargets.Remove(target);
        }

        public void RemoveInputInteractionTargets(List<IInputEvent> targets)
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

        public virtual void Update()
        {
            if (mControllTargets.Count == 0)
            {
                return;
            }

            // TODO : ÀÔ·Â ¸Ê ¼öÁ¤
            mInputAxisRawMap = new Dictionary<string, float>();
            // 
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

                    if (mInputValue != 0)
                    {
                        mInputInteraction.Invoke(mInputValue);
                    }
                }
            }
        }
    }
}