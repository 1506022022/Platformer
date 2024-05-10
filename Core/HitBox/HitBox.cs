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
            foreach (var collider in colliders)
            {
                collider.SetHitEvnet(hitCallback);
            }
        }

        public void SetHitCallback(List<string> filterColliderNames, HitEvent hitCallback)
        {
            var colliders = GetCollidersAs(filterColliderNames, mHitControll);
            foreach (var collider in colliders)
            {
                collider.SetHitEvnet(hitCallback);
            }
        }

        public void SetAttackCollidersFlags(List<string> filterColliderNames, HitBoxFlags flags)
        {
            var colliders = GetCollidersAs(filterColliderNames, mAttackControll);
            foreach (var collider in colliders)
            {
                collider.HitBoxFlag.SetFlag(flags);
            }
        }

        public void EnableAttackColliders(List<string> filterColliderNames)
        {
            var colliders = GetCollidersAs(filterColliderNames, mAttackControll);
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }
        }

        public void DisableAttackColliders(List<string> filterColliderNames)
        {
            var colliders = GetCollidersAs(filterColliderNames, mAttackControll);
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
        }

        public void EnableHitColliders(List<string> filterColliderNames)
        {
            var colliders = GetCollidersAs(filterColliderNames, mHitControll);
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }
        }

        public void DisableHitColliders(List<string> filterColliderNames)
        {
            var colliders = GetCollidersAs(filterColliderNames, mHitControll);
            foreach (var collider in colliders)
            {
                collider.enabled = false;
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

        void DI(HitBoxControll controll)
        {
            controll.SetActor(mCharacter);

            foreach (var collider in controll.Colliders)
            {
                Debug.Assert(collider.HitCallback == null);
                collider.HitCallback = collider.HitBoxFlag.IsAttacker() ?
                                       OnAttack :
                                       OnHit;
            }
        }

        List<HitBoxCollider> GetCollidersAs(List<string> filterColliderNames, HitBoxControll controll)
        {
            List<HitBoxCollider> list = new List<HitBoxCollider>();
            foreach (var filter in filterColliderNames)
            {
                var colliders = controll.GetCollidersAs(filter);
                foreach (var collider in colliders)
                {
                    if (!list.Contains(collider))
                    {
                        list.Add(collider);
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