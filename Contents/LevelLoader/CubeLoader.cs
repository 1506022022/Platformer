using PlatformGame.Character.Controller;
using PlatformGame.Input;
using UnityEngine;
using static PlatformGame.Input.ActionKey;

namespace PlatformGame.Contents.Loader
{
    public class CubeLoader : MonoBehaviour, ILevelLoader
    {
        public WorkState State { get; private set; }
        [SerializeField] PlayerCharacterController mCubeController;

        void Awake()
        {
            Debug.Assert(mCubeController != null);
        }

        void Update()
        {
            if (State != WorkState.Action)
            {
                return;
            }
            
            var map = ActionKey.GetAxisRawMap();
            if (map[GUARD])
            {
                State = WorkState.Ready;
                mCubeController.SetControll(false);
            }
        }

        public void LoadNext()
        {
            State = WorkState.Action;
            mCubeController.SetControll(true);
        }
    }
}
