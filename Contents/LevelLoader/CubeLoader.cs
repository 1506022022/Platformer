using UnityEngine;

namespace Platformer.Contents
{
    public class CubeLoader : MonoBehaviour, ILevelLoader
    {
        public AbilityState State {  get; private set; }
        float mLastStoped;
        CubeController mController;
        [SerializeField] RotateAbility mCube;
        void Awake()
        {
            mController = new CubeController(mCube);
        }
        void Update()
        {
            if( State == AbilityState.Colltime &&
                mLastStoped + 1f < Time.time)
            {
                State = AbilityState.Ready;
            }
            if(State != AbilityState.Action)
            {
                return;
            }
            if (mController.State == AbilityState.Action)
            {
                mController.Update();
            }
            else
            {
                State = AbilityState.Colltime;
                mLastStoped = Time.time;
            }
        }

        public void LoadNext()
        {
            State = AbilityState.Action;
            mController.Start();
        }
    }
}
