using PlatformGame.Character.Collision;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/AbilityAction/ReverseReflectionForce")]
    public class ReverseReflectionForce : ReflectionForce
    {
        public override void Action(CollisionData collision)
        {
            var temp = collision.Victim;
            collision.Victim = collision.Attacker;
            collision.Attacker = temp;

            base.Action(collision);
        }
    }

}
