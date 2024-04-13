using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Input.Controller
{
    [DisallowMultipleComponent]
    public class Controller : MonoBehaviour
    {
        public bool IsActive { get; set; }
        string mKeyName;
        float mKeyInputValue;
        UnityAction<float> mInputEvent;
        Dictionary<string, float> mAxisRawMap;
        List<IInputInteraction> mInputInteractionTargets = new List<IInputInteraction>();

        public void AddInputInteractionTarget(IInputInteraction target)
        {
            mInputInteractionTargets.Add(target);
        }
        public void AddInputInteractionTargets(List<IInputInteraction> targets)
        {
            mInputInteractionTargets.AddRange(targets);
        }
        public void RemoveInputInteractionTarget(IInputInteraction target)
        {
            mInputInteractionTargets.Remove(target);
        }
        public void RemoveInputInteractionTargets(List<IInputInteraction> targets)
        {
            foreach (var target in targets)
            {
                mInputInteractionTargets.Remove(target);
            }
        }
        void Awake()
        {
            var inputInteractions = GetComponents<IInputInteraction>().ToList();
            AddInputInteractionTargets(inputInteractions);
        }
        void Update()
        {
            if (!IsActive) return;

            mAxisRawMap = ActionKey.GetAxisRawMap();
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
                    mKeyInputValue = mAxisRawMap[mKeyName];

                    mInputEvent.Invoke(mKeyInputValue);
                }
            }
        }
    }
}
