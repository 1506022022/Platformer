using System.Collections.Generic;
using UnityEngine.Events;

namespace RPG.Input
{
    public interface IInputInteraction
    {
        public bool IsAbleNow();
        public Dictionary<string, UnityAction<float>> InputEventMap { get; }
    }
}
