using System;

namespace Platformer.Core
{
    [Flags, Serializable]
    public enum AbilityPositionFlags
    {
        None = 0,
        Idle = 1 << 1,
        Move = 1 << 2,
        Jump = 1 << 3,
        Fall = 1 << 4,
        Action = 1 << 5
    }
    public enum AbilityState
    {
        Idle,
        Move,
        Jump,
        Fall,
        Action
    }
}
