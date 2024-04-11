using System.Collections.Generic;
using UnityEngine.Events;

namespace RPG.Input
{
    public interface IInputInteraction
    {
        public bool IsAble { get; }
        public Dictionary<string, UnityAction<float>> InputEventMap { get; }
    }
}
