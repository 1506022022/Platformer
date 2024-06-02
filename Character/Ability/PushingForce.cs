using UnityEngine;
using static PlatformGame.Character.Collision.AttributeFlags;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/Ability/PushingForce")]
    public class PushingForce : Ability
    {
        public float PowerMultiply = 300f;
        public float UpperPower = 3f;

        public override void UseAbility(AbilityCollision collision)
        {
            var victim = collision.Victim;
            var rigid = victim.Rigid;
            if (rigid == null)
            {
                return;
            }

            var attacker = collision.Caster;
            var force = attacker.Model.forward;
            force.y = UpperPower;
            force *= PowerMultiply;
            PushingTo(victim, force);
        }

        public static void PushingTo(Character victim, Vector3 force)
        {
<<<<<<< HEAD
            if (!victim.Attribute.IsInclude(NonStatic))
=======
            if(!victim.Attribute.IsInclude(NonStatic))
>>>>>>> 55905e167fdf216c12329ed6e479be5122586bc9
            {
                return;
            }

            if (victim.transform.parent != null)
            {
                victim.transform.SetParent(null, true);
            }

            victim.Movement.RemoveMovement();
            victim.Rigid.AddForce(force);
        }

    }
}