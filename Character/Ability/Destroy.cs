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
<<<<<<< HEAD
=======
            if (!victim.Attribute.IsInclude(AttributeFlags.Destructibility))
            {
                return;
            }
>>>>>>> 55905e167fdf216c12329ed6e479be5122586bc9
            DestroyTo(victim);
        }

        public static void DestroyTo(Character character)
        {
<<<<<<< HEAD
            if (!character.Attribute.IsInclude(AttributeFlags.Destructibility))
            {
                return;
            }
=======
>>>>>>> 55905e167fdf216c12329ed6e479be5122586bc9
            GameObject.Destroy(character.gameObject);
        }
    }
}