using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace PlatformGame.Character.Collision
{
    [Flags, Serializable]
    public enum AttributeFlags
    {
        None = 0,
        NonStatic = 1 << 0,
        Destructibility = 1 << 1,
        Permeability = 1 << 2,
        Stickiness = 1 << 3
    }

    [Serializable]
    public class AttributeFlag
    {
        [SerializeField] AttributeFlags mFlags;
        public AttributeFlags Flags
        {
            get => mFlags;
            private set => mFlags = value;
        }

        // TODO : °³¼±
        public void SetFlag(AttributeFlags flags, Character character)
        {
            Flags = flags;
            character.Rigid.isKinematic = !IsInclude(AttributeFlags.NonStatic);

            var layer = 1 << LayerMask.NameToLayer("Ignore Raycast");
            if (IsInclude(AttributeFlags.Permeability))
            {
                character.Rigid.excludeLayers |= layer;
            }
            else
            {
                character.Rigid.excludeLayers &= ~LayerMask.NameToLayer("Ignore Raycast");
            }

            if (IsInclude(AttributeFlags.Stickiness) &&
                character.transform.GetComponent<StickyComponent>() == null)
            {

                character.transform.AddComponent<StickyComponent>();
            }

        }
        // TODOEND

        public bool Equals(AttributeFlags flags)
        {
            return flags == Flags;
        }

        public bool IsInclude(AttributeFlags flag)
        {
            Debug.Assert((flag & (flag - 1)) == 0 && flag != 0, $"Only one flag should be used : {flag}");
            return (Flags & flag) == flag;
        }

        public static List<HitBoxCollider> GetCollidersAs(Dictionary<string, HitBoxCollider> list,
            List<string> filterColliderNames)
        {
            var colliders = new List<HitBoxCollider>();

            if (filterColliderNames.Any(x => x.Equals("*")))
            {
                colliders = list.Values.ToList();
            }
            else
            {
                foreach (var colliderName in filterColliderNames)
                {
                    Debug.Assert(list.ContainsKey(colliderName), $"{colliderName} is not found in HitBoxColliders.");
                    colliders.Add(list[colliderName]);
                }
            }

            return colliders;
        }
    }
}