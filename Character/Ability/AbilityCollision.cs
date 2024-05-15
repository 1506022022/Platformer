using System;
using PlatformGame.Character.Collision;

namespace PlatformGame.Character.Combat
{
    [Serializable]
    public struct AbilityCollision
    {
        public CollisionData Data;
        public Ability Ability;

        public AbilityCollision(CollisionData data, Ability ability)
        {
            Data = data;
            Ability = ability;
        }
    }
}