using PlatformGame.Character.Collision;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/AbilityAction/ReflectionForce")]
    public class ReflectionForce : AbilityAction
    {
        public float PowerMultiply = 300f;
        public float UpperPower = 3f;
        public override void Action(CollisionData collision)
        {
            var attacker = collision.Attacker;
            var victim = collision.Victim;
            
            if (victim.transform.parent != null)
            {
                victim.transform.SetParent(null, true);
            }
            
            victim.Movement.RemoveMovement();
            
            var rigid = victim.GetComponent<Rigidbody>();
            if (rigid == null)
            {
                return;
            }
            var dir = attacker.transform.forward;
            dir.y = UpperPower;
            rigid.AddForce(dir * PowerMultiply);
        }

    }
}
