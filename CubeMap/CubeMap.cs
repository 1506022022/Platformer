using PlatformGame.Character.Controller;
using System;
using UnityEngine;

namespace PlatformGame.Character
{
    public enum CubeMapState
    {
        Forward, Backward, Left, Right, Up, Down
    }

    public class CubeMap : MonoBehaviour
    {
        public static CubeMap Instance { get; private set; }

        [SerializeField] CubeMapState mState;
        [SerializeField] PlayerCharacterController cubeMapController;
        public CubeMapState State
        {
            get => mState;
            set => mState = value;
        }

        [VisibleEnum(typeof(CubeMapState))]
        public void ChangeState(int newState)
        {
            Debug.Assert(0 <= newState &&
                newState < Enum.GetValues(typeof(CubeMapState)).Length);
            State = (CubeMapState)newState;
            foreach (var obj in CubeMapObject.Objects)
            {
                obj.gameObject.SetActive(true);
                obj.OnChanged(State);
            }
        }

        public void OnChanging()
        {
            foreach (var obj in CubeMapObject.Objects)
            {
                obj.gameObject.SetActive(false);
            }
        }

        void Awake()
        {
            Instance = this;
            cubeMapController.KeyInputEvent.AddListener((a,b)=>OnChanging());
        }

    }
}
