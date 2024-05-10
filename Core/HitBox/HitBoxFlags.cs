using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PlatformGame.Character.Collision
{
    [Flags, Serializable]
    public enum HitBoxFlags
    {
        None = 0,
        Attacker = 1 << 0
    }

    [Serializable]
    public class HitBoxFlag
    {
        public HitBoxFlags Flags;

        public void SetFlag(HitBoxFlags flags)
        {
            Flags = flags;
        }

        public bool Equals(HitBoxFlags flags)
        {
            return flags == Flags;
        }

        public bool CanAttack(HitBoxCollider targetHitBox)
        {
            return IsAttacker() &&
                   !targetHitBox.IsDelay &&
                   !targetHitBox.HitBoxFlag.IsAttacker();
        }

        public bool IsAttacker()
        {
            return (Flags & HitBoxFlags.Attacker) == HitBoxFlags.Attacker;
        }

        public bool IsInclude(HitBoxFlags flags)
        {
            return (Flags & flags) == flags;
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

        public static List<HitBoxCollider> GetCollidersAs(List<HitBoxCollider> list, HitBoxFlags filterFlags)
        {
            var colliders = list.Where(x => x.HitBoxFlag.IsInclude(filterFlags))
                .ToList();
            return colliders;
        }
    }
}