using UnityEngine;
using PlatformGame.Character.Collision;
using System;

namespace PlatformGame.Character.Combat
{

    [Serializable]
    public struct AbilityActionPair
    {
        public HitBoxCollider Collider;
        public AbilityAction Action;
    }

    public abstract class AbilityAction : ScriptableObject
    {
        public abstract void Action(CollisionData collision);
    }
}