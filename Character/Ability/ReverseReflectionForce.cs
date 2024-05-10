using PlatformGame.Character.Collision;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/AbilityAction/ReverseReflectionForce")]
    public class ReverseReflectionForce : ReflectionForce
    {
        public override void Action(CollisionData collision)
        {
            (collision.Victim, collision.Attacker) = (collision.Attacker, collision.Victim);

            base.Action(collision);
        }
    }
}