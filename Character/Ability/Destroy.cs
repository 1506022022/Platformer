using PlatformGame.Character.Collision;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/Ability/Destroy")]
    public class Destroy : Ability
    {
        public override void UseAbility(AbilityCollision collision)
        {
            var victim = collision.Victim;
            if (!victim.Attribute.IsInclude(AttributeFlags.Destructibility))
            {
                return;
            }
            DestroyTo(victim);
        }

        public static void DestroyTo(Character character)
        {
            GameObject.Destroy(character.gameObject);
        }
    }
}