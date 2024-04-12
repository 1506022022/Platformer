using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace RPG.Input.Controller
{
    public class Controller
    {
        public bool IsActive { get; set; }
        string mKeyName;
        float mKeyInputValue;
        UnityAction<float> mInputEvent;
        List<IInputInteraction> mInputInteractionTargets = new List<IInputInteraction>();

        public virtual void Update()
        {
            if (!IsActive) return;

            Dictionary<string, float> axisRawMap = ActionKey.GetAxisRawMap();
            foreach (var interactionTarget in mInputInteractionTargets.ToList())
            {
                if (!interactionTarget.IsAble())
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
        public void AddInputInteractionTarget(IInputInteraction target)
        {
            mInputInteractionTargets.Add(target);
        }
        public void RemoveInputInteractionTarget(IInputInteraction target)
        {
            mInputInteractionTargets.Remove(target);
        }
    }
}
