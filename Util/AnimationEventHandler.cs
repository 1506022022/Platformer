using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Tool.EventHandler
{
    public class AnimationEventHandler : MonoBehaviour
    {
        [SerializeField] UnityEvent mOnFrameEvent;

        public void OnFrameEvent()
        {
            mOnFrameEvent.Invoke();
        }
    }
}