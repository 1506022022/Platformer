using UnityEngine;
using UnityEngine.Events;

namespace RPG.Character
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
