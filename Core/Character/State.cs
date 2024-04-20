using System;
using System.Collections.Generic;

namespace RPG.Character
{
    [Serializable]
    public enum State { Idle, Running, Jumping, Falling, Attack, Die }

    public struct AnimationTrigger
    {
        public static readonly string FALL = "Fall";
        public static readonly string IDLE = "Idle";
        public static readonly string JUMP = "Jump";
        public static readonly string LAND = "Land";
        public static readonly string RUN = "Run";
        public static readonly string WALK = "Walk";
        public static readonly string ATTACK = "Attack";
        public static readonly string GUARD = "Guard";
        public static readonly string PARRYING = "Parrying";

        public static readonly Dictionary<string, State> STATE_MAP = new Dictionary<string, State>()
        {
            { IDLE,     State.Idle },
            { FALL,     State.Falling },
            { JUMP,     State.Jumping },
            { LAND,     State.Falling },
            { RUN,      State.Running },
            { WALK,     State.Running },
            { ATTACK,   State.Attack },
            { GUARD,    State.Attack},
            { PARRYING, State.Attack }
        };
    }
}
