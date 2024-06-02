using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Contents
{
    public class Map : MonoBehaviour
    {
        static Map mInstance;

        public static Map Instance
        {
            get
            {
                Debug.Assert(mInstance);
                return mInstance;
            }
            private set => mInstance = value;
        }

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
            Debug.Assert(maps.Count() == 1, $"Not unique : {gameObject.name}, {maps.Count()}");
#endif
        }
    }
}