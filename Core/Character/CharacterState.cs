using System;
using System.Collections.Generic;

namespace PlatformGame.Character
{
    [Flags, Serializable]
    public enum CharacterStateFlags
    {
        None = 0,
        Idle = 1 << 1,
        Move = 1 << 2,
        Jump = 1 << 3,
        Fall = 1 << 4,
        Action = 1 << 5
    }

    [Serializable]
    public enum CharacterState
    {
        Idle,
        Walk,
        Running,
        Jumping,
        Falling,
        Land,
        Attack,
        Die
    }
    public static class StateCheck
    {
        public static bool Equals(CharacterState state, CharacterStateFlags flags)
        {
            switch (state)
            {
                case CharacterState.Idle: return (flags & CharacterStateFlags.Idle) == CharacterStateFlags.Idle;
                case CharacterState.Walk: return (flags & CharacterStateFlags.Move) == CharacterStateFlags.Move;
                case CharacterState.Running: return (flags & CharacterStateFlags.Move) == CharacterStateFlags.Move;
                case CharacterState.Jumping: return (flags & CharacterStateFlags.Jump) == CharacterStateFlags.Jump;
                case CharacterState.Falling: return (flags & CharacterStateFlags.Fall) == CharacterStateFlags.Fall;
                case CharacterState.Attack: return (flags & CharacterStateFlags.Action) == CharacterStateFlags.Action;
                default: return false;

            }
        }
    }

    public struct AnimationTrigger
    {
        public static readonly string IDLE = "Idle";
        public static readonly string WALK = "Walk";
        public static readonly string RUN = "Run";
        public static readonly string JUMP = "Jump";
        public static readonly string FALL = "Fall";
        public static readonly string LAND = "Land";
        public static readonly string ATTACK = "Attack";
        public static readonly string GUARD = "Guard";
        public static readonly string PARRYING = "Parrying";

        public static readonly Dictionary<CharacterState, string> STATE_MAP = new Dictionary<CharacterState, string>()
        {
            { CharacterState.Idle, IDLE },
            { CharacterState.Falling, FALL },
            { CharacterState.Jumping, JUMP },
            { CharacterState.Land, LAND },
            { CharacterState.Walk, WALK },
            { CharacterState.Running, RUN }
        };
    }
}