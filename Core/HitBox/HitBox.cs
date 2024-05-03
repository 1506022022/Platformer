using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Platformer.Core.HitBoxFlag;

namespace Platformer.Core
{
    public delegate void HitBoxCallback(HitBox victim, HitBox attacker);
    public class HitBox : MonoBehaviour
    {
        static List<HitBox> mHitedInstances = new List<HitBox>();
        public bool UseSyncDelay;
        public float HitDelay;
        public HitBoxFlag Flags;
        HitBoxCallback mAttackedCallback;
        Dictionary<string, HitBoxCollider> mHBCollidersDictionary;
        [SerializeField] List<HitBoxCollider> mHitBoxColliders;

        public void SetAttackCallback(List<string> filterColliderNames, HitBoxCallback attackedCallback)
        {
            ClearAttackCallback();
            mAttackedCallback = attackedCallback;
            var HBColliders = GetCollidersAs(mHBCollidersDictionary, filterColliderNames);
            foreach ( var collider in HBColliders )
            {
                Debug.Assert(collider.Flags.IsAttacker());
                collider.enabled = true;
            }
        }

        public void ClearAttackCallback()
        {
            mAttackedCallback = null;
            var HBColliders = GetCollidersAs(mHitBoxColliders, HitBoxFlags.Attacker);
            foreach(var collider in HBColliders)
            {
                collider.enabled = false;
            }
        }

        void Awake()
        {
            Debug.Assert(mHitBoxColliders != null);
            Debug.Assert(mHitBoxColliders.Count > 0);
            CreateDictionary();
            mHitBoxColliders.ForEach(x => x.HitedEventCallback = OnHited);
            mHitBoxColliders.ForEach(x => x.Flags = Flags);
            if(UseSyncDelay)
            {
                mHitBoxColliders.ForEach(x => x.HitDelay = HitDelay);
            }
            ClearAttackCallback();
            mHitedInstances.Add(this);
        }
        void OnDestroy()
        {
            mHitedInstances.Remove(this);
        }
        void CreateDictionary()
        {
            mHBCollidersDictionary = new Dictionary<string, HitBoxCollider>();
            foreach (var HBCollider in mHitBoxColliders)
            {
                Debug.Assert(!mHBCollidersDictionary.ContainsKey(HBCollider.Name),$"{HBCollider.Name} is Duplicate.");
                mHBCollidersDictionary.Add(HBCollider.Name, HBCollider);
            }
        }
        void OnHited(HitBoxCollider victim, HitBoxCollider attacker)
        {
            HitBox victimHitBox = mHitedInstances.Where(hitBox => hitBox.mHitBoxColliders.Any(coll => coll.Equals(victim)))
                                                 .FirstOrDefault(); 
            Debug.Assert(victimHitBox);

            HitBox attackerHitBox = mHitedInstances.Where(hitBox => hitBox.mHitBoxColliders.Any(coll => coll.Equals(attacker)))
                                                   .FirstOrDefault();
            Debug.Assert(attackerHitBox);

            if (UseSyncDelay)
            {
                mHitBoxColliders.ForEach(x => x.StartDelay());
            }

            mAttackedCallback?.Invoke(victimHitBox, attackerHitBox);
        }
    }
}