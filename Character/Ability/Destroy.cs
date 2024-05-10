using PlatformGame.Character.Collision;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/AbilityAction/Destroy")]
    public class Destroy : AbilityAction
    {
        public UnityEvent<CollisionData> DestroyEvent;

        public override void Action(CollisionData collision)
        {
            DestroyEvent.Invoke(collision);
        }

        public void DestroyVictim(CollisionData collision)
        {
            GameObject.Destroy(collision.Victim.gameObject);
        }

        public void DestroyAttacker(CollisionData collision)
        {
            GameObject.Destroy(collision.Attacker.gameObject);
        }

    }

}
