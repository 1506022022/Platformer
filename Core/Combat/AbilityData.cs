using PlatformGame.Character.Collision;
using PlatformGame.Character.Movement;
using System;

namespace PlatformGame.Character.Combat
{
    [Serializable]
    public struct AbilityData
    {
        public uint ID;
        public string Name;
        public float ActionDelay;
        public CharacterState BeState;
        public CharacterStateFlags AllowedState;
        public AbilityAction HitedEvent;
        public MovementAction MovementAction;
        public HitBoxData HitBoxData;
    }
}