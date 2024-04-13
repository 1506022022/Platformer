using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Input
{
    public class InputInteraction : MonoBehaviour, IInputInteraction
    {
        public Dictionary<string, UnityAction<float>> InputEventMap
        {
            get;
            protected set;
        }
        protected virtual void Awake()
        {
            MappingInputEvent();
        }
        public virtual bool IsAble()
        {
            Debug.Assert(false, "Do not Override this Method");
            return false;
        }
        protected virtual void MappingInputEvent() { }
    }
}

