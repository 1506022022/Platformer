using UnityEngine;

namespace Platformer.Contents
{
    public class LevelLoader : ILevelLoader
    {
        public AbilityState State
        {
            get
            {
                return mEndTime <= Time.time ? AbilityState.Ready : AbilityState.Action;
            }
        }
        float mEndTime;

        public void LoadNext()
        {
            mEndTime = Time.time + 3f;
        }
    }
}
