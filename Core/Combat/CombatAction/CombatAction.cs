using UnityEngine;

namespace Platformer.Core
{
    public abstract class CombatAction : ScriptableObject
    {
        public abstract void Action(HitBox victim, HitBox attacker);
    }
}

