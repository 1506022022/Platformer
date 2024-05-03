using System;

namespace Platformer.Core
{
    [Serializable]
    public struct CombatData
    {
        public uint ID;
        public float ActionTime;
        public string AnimationName;
        public AbilityPositionFlags AllowedState;
        public HitBoxData HitBoxData;
        public CombatAction HitedEvent;
    }
}