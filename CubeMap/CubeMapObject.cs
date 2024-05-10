using PlatformGame.Character.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Character
{
    [Serializable]
    public struct CubMapChange
    {
        public List<AbilityActionPair> Abilities;
        public UnityEvent ChangeEvent;
    }

    public class CubeMapObject : MonoBehaviour
    {
        static List<CubeMapObject> mObjects = new();
        public static List<CubeMapObject> Objects => mObjects;

        public UnityEvent ForwardChange;
        public UnityEvent BackwardChange;
        public UnityEvent RightChange;
        public UnityEvent LeftChange;
        public UnityEvent UpChange;
        public UnityEvent DownChange;

        public void OnChanged(CubeMapState state)
        {
            var change = new UnityEvent();
            switch(state)
            {
                case CubeMapState.Forward:
                    change = ForwardChange;
                    break;
                    case CubeMapState.Backward:
                    change = BackwardChange;
                    break;
                    case CubeMapState.Left:
                    change = LeftChange;
                    break;
                    case CubeMapState.Right:
                    change = RightChange;
                    break;
                    case CubeMapState.Up:
                    change = UpChange;
                    break;
                    case CubeMapState.Down:
                    change = DownChange;
                    break;
                default:
                    Debug.Assert(false, $"정의되지 않은 값 : {nameof(CubeMapState)}, {state}.");
                    break;
            }

            change.Invoke();

        }

        void Awake()
        {
            Objects.Add(this);
        }

        void Start()
        {
            OnChanged(CubeMap.Instance.State);
        }

        void OnDestroy()
        {
            Objects.Remove(this);
        }

    }
}

