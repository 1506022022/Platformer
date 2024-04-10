using System.Collections.Generic;

namespace RPG.Character
{
    public struct AnimationTrigger
    {
        public static readonly string FALL = "Fall";
        public static readonly string IDLE = "Idle";
        public static readonly string JUMP = "Jump";
        public static readonly string LAND = "Land";
        public static readonly string RUN = "Run";
        public static readonly string WALK = "Walk";

        public static readonly Dictionary<string, State> STATE_MAP = new Dictionary<string, State>()
        {
            { FALL, State.Falling },
            { IDLE, State.Idle },
            { JUMP, State.Jumping },
            { LAND, State.Falling },
            { RUN, State.Running },
            { WALK, State.Running }
        };
    }
}
