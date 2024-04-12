using System.Collections.Generic;
using UnityEngine.Events;

namespace RPG.Input
{
    public interface IInputInteraction
    {
        public bool IsAble();
        public Dictionary<string, UnityAction<float>> InputEventMap { get; }
    }
}
