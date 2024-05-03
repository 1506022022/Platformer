using System;
using System.Collections.Generic;

namespace RPG.Character
{
    [Serializable]
    public enum State { Idle, Walk, Running, Jumping, Falling, Land, Attack, Die }

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

        public static readonly Dictionary<State, string> STATE_MAP = new Dictionary<State, string>()
        {
            { State.Idle, IDLE },
            { State.Falling , FALL },
            { State.Jumping , JUMP },
            { State.Land , LAND },
            { State.Walk , WALK },
            { State.Running , RUN }
        };
    }
}
