using UnityEngine;

namespace Platformer.Core
{
    [CreateAssetMenu(menuName ="Custom/CombatAction/ReflectionForce",fileName ="ReflectionForce")]
    public class ReflectionForce : CombatAction
    {
        public override void Action(HitBox victim, HitBox attacker)
        {
            var rigid = victim.GetComponent<Rigidbody>();
            if (rigid != null && rigid.velocity.magnitude > 0f)
            {
                Vector3 dir = attacker.transform.forward;
                dir.y = 3;
                rigid.AddForce(dir * 300f);
            }
        }
    }
}
