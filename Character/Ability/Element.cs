using PlatformGame.Character.Collision;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/AbilityAction/Element")]
    public class Element : AbilityAction
    {
        public string TargetTag = "Fire";
        public UnityEvent<CollisionData> CollisionEvent;

        public override void Action(CollisionData collision)
        {
            var attacker = collision.Attacker;

            if (!attacker.CompareTag(TargetTag))
            {
                return;
            }

            CollisionEvent.Invoke(collision);
        }

        public void Burn(CollisionData collision)
        {
            var attacker = collision.Attacker;
            var victim = collision.Victim;

            var obj = Instantiate(attacker);
            obj.transform.position = victim.transform.position;
            Destroy(victim.gameObject);
        }

    }
}