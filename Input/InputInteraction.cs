using System.Collections.Generic;
using UnityEngine.Events;

namespace RPG.Input
{
    public abstract class InputInteraction : IInputInteraction
    {
        public virtual bool IsAble
        {
            get;
        }
        public Dictionary<string, UnityAction<float>> InputEventMap
        {
            get;
            protected set;
        }
        public InputInteraction()
        {
            MappingInputEvent();
        }
        protected abstract void MappingInputEvent();

    }
}

