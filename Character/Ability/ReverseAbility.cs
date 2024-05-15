using PlatformGame.Character.Collision;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/Ability/ReverseAbility")]
    public class ReverseAbility : Ability
    {
        public Ability AbilityAction;

        public override void UseAbility(CollisionData collision)
        {
            (collision.Attacker, collision.Victim) = (collision.Victim, collision.Attacker);
            AbilityAction.UseAbility(collision);
        }

    }
}