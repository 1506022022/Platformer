using PlatformGame.Character.Collision;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/Ability/Destroy")]
    public class Destroy : Ability
    {
        public override void UseAbility(CollisionData collision)
        {
            var victim = collision.Victim;
            DestroyTo(victim.gameObject);
        }

        public static void DestroyTo(GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
        }
    }
}