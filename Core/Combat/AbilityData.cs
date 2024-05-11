using PlatformGame.Character.Collision;
using PlatformGame.Character.Movement;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/AbilityData")]
    public class AbilityData : ScriptableObject
    {
        public uint ID;
        public string Name;
        public float ActionDelay;
        public CharacterState BeState;
        public CharacterStateFlags AllowedState;
        public AbilityAction Ability;
        public MovementAction Movement;
        public HitBoxData HitBoxData;
    }
}