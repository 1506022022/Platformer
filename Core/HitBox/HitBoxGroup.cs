using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame.Character.Collision
{
    public delegate void AttackCallback(Character victim, uint actionID);

    public class HitBoxGroup : MonoBehaviour
    {
        readonly Dictionary<HitBoxCollider, uint> mActionIDs = new();
        Character mCharacter;
        AttackCallback mAttackCallback;
        [SerializeField] HitBoxControll mHitControll;
        [SerializeField] HitBoxControll mAttackControll;

        public void AddAttackCallback(AttackCallback callback)
        {
            mAttackCallback += callback;
        }

        public void SetAttackEvent(List<string> filterColliderNames, HitEvent hitEvent, uint actionID)
        {
            var colliders = GetCollidersAs(filterColliderNames, mAttackControll);
            foreach (var hitBoxCollider in colliders)
            {
                hitBoxCollider.SetHitEvent(hitEvent);
                mActionIDs[hitBoxCollider] = actionID;
            }
        }

        public void SetAttackCollidersFlags(List<string> filterColliderNames, HitBoxFlags flags)
        {
            var colliders = GetCollidersAs(filterColliderNames, mAttackControll);
            foreach (var hitBoxCollider in colliders)
            {
                hitBoxCollider.HitBoxFlag.SetFlag(flags);
            }
        }

        void OnHit(CollisionData collision, HitBoxCollider subject)
        {
            mHitControll.StartDelay();
        }

        void OnAttack(CollisionData collision, HitBoxCollider subject)
        {
            mAttackControll.StartDelay();

            if (mAttackCallback == null)
            {
                return;
            }

            var victim = collision.Victim;
            var actionID = mActionIDs[subject];
            mAttackCallback.Invoke(victim, actionID);
        }

        void InjectionDependencyInto(HitBoxControll hitBoxControll)
        {
            hitBoxControll.SetActor(mCharacter);

            foreach (var hitBoxCollider in hitBoxControll.Colliders)
            {
                hitBoxCollider.AddCallback((collision) =>
                {
                    if (hitBoxCollider.HitBoxFlag.IsAttacker())
                    {
                        OnAttack(collision, hitBoxCollider);
                    }
                    else
                    {
                        OnHit(collision, hitBoxCollider);
                    }
                });
            }
        }

        static List<HitBoxCollider> GetCollidersAs(List<string> filterColliderNames, HitBoxControll hitBoxControll)
        {
            var list = new List<HitBoxCollider>();
            foreach (var filter in filterColliderNames)
            {
                var colliders = hitBoxControll.GetCollidersAs(filter);
                foreach (var hitBoxCollider in colliders)
                {
                    if (!list.Contains(hitBoxCollider))
                    {
                        list.Add(hitBoxCollider);
                    }
                }
            }

            return list;
        }

        void Awake()
        {
            mCharacter = GetComponent<Character>();
            InjectionDependencyInto(mHitControll);
            InjectionDependencyInto(mAttackControll);

            foreach (var hitBoxCollider in mAttackControll.Colliders)
            {
                mActionIDs.Add(hitBoxCollider, 0);
            }
        }
    }
}