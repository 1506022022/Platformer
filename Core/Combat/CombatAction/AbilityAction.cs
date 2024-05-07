using PlatformGame.Character.Collision;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    public abstract class AbilityAction : ScriptableObject
    {
        public abstract void Action(HitBox victim, HitBox attacker);
    }
}