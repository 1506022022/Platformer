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
            private set
            {
                mInstance = value;
            }
        }
        static Map mInstance;
        [SerializeField] Stage mStage;

        public void AddListenerClearEvent(UnityAction action)
        {
            mStage.AddListenerClearEvent(action);
        }
        void Awake()
        {
            mInstance = this;
#if DEVELOPMENT
            var maps = FindObjectsOfType<Map>();
            Debug.Assert(maps.Count() == 1);
#endif
        }
    }
}
