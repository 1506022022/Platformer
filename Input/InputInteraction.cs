using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Input
{
    public class InputInteraction : IInputInteraction
    {
        public Dictionary<string, UnityAction<float>> InputEventMap
        {
            get;
            protected set;
        }
        public InputInteraction()
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

