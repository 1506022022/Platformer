using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame.Character.Collision
{
    public class HitBox : MonoBehaviour
    {
        [SerializeField] HitBoxControll mHitControll;
        [SerializeField] HitBoxControll mAttackControll;
        Character mCharacter;

        public void SetAttackCallback(List<string> filterColliderNames, HitEvent hitCallback)
        {
            var colliders = GetCollidersAs(filterColliderNames, mAttackControll);
            foreach (var hitBoxCollider in colliders)
            {
                hitBoxCollider.SetHitEvent(hitCallback);
            }
        }

        public void SetHitCallback(List<string> filterColliderNames, HitEvent hitCallback)
        {
            var colliders = GetCollidersAs(filterColliderNames, mHitControll);
            foreach (var hitBoxCollider in colliders)
            {
                hitBoxCollider.SetHitEvent(hitCallback);
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

        public void EnableAttackColliders(List<string> filterColliderNames)
        {
            var colliders = GetCollidersAs(filterColliderNames, mAttackControll);
            foreach (var hitBoxCollider in colliders)
            {
                hitBoxCollider.enabled = true;
            }
        }

        public void DisableAttackColliders(List<string> filterColliderNames)
        {
            var colliders = GetCollidersAs(filterColliderNames, mAttackControll);
            foreach (var hitBoxCollider in colliders)
            {
                hitBoxCollider.enabled = false;
            }
        }

        public void EnableHitColliders(List<string> filterColliderNames)
        {
            var colliders = GetCollidersAs(filterColliderNames, mHitControll);
            foreach (var hitBoxCollider in colliders)
            {
                hitBoxCollider.enabled = true;
            }
        }

        public void DisableHitColliders(List<string> filterColliderNames)
        {
            var colliders = GetCollidersAs(filterColliderNames, mHitControll);
            foreach (var hitBoxCollider in colliders)
            {
                hitBoxCollider.enabled = false;
            }
        }

        void OnHit(HitBoxCollider subject)
        {
            mHitControll.StartDelay();
        }

        void OnAttack(HitBoxCollider subject)
        {
            mAttackControll.StartDelay();
        }

        void DI(HitBoxControll hitBoxControll)
        {
            hitBoxControll.SetActor(mCharacter);

            foreach (var hitBoxCollider in hitBoxControll.Colliders)
            {
                Debug.Assert(hitBoxCollider.HitCallback == null);
                hitBoxCollider.HitCallback = hitBoxCollider.HitBoxFlag.IsAttacker() ? OnAttack : OnHit;
            }
        }

        List<HitBoxCollider> GetCollidersAs(List<string> filterColliderNames, HitBoxControll hitBoxControll)
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
            DI(mHitControll);
            DI(mAttackControll);
        }
    }
}