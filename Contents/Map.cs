using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Contents
{
    public class Map : MonoBehaviour
    {
        public static Map Instance
        {
            get
            {
                Debug.Assert(mInstance);
                return mInstance;
            }
            protected set
            {
                mInstance = value;
            }
        }
        static Map mInstance;
        [SerializeField] UnityEvent mClearEvent = new UnityEvent();

        public void AddListenerClearEvent(UnityAction action)
        {
            mClearEvent.AddListener(action);
        }
        protected virtual void Awake()
        {
            mInstance = this;
#if UNITY_EDITOR
            var maps = FindObjectsOfType<Map>();
            Debug.Assert(maps.Count() == 1);
#endif
        }
        protected virtual void Start()
        {
        }
        protected void Clear()
        {
            mClearEvent.Invoke();
        }
    }
}
