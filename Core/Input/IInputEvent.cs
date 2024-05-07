using System.Collections.Generic;
using UnityEngine.Events;

namespace PlatformGame.Input
{
    public interface IInputEvent
    {
        public bool IsAbleNow();
        public Dictionary<string, UnityAction<float>> InputEventMap { get; }
    }
}
