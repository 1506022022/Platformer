using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Core
{
    public delegate void HitBoxColliderCallback(HitBoxCollider victim, HitBoxCollider attacker);

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class HitBoxCollider : MonoBehaviour
    {
        public float HitDelay;
        public HitBoxFlag Flags;
        public HitBoxColliderCallback HitedEventCallback;
        public bool IsDelay
        {
            get => Time.time < mLastHitTime + HitDelay;
        }
        
        [SerializeField] string mName;
        public string Name
        {
            get
            {
                Debug.Assert(!string.IsNullOrEmpty(mName));
                return mName;
            }
        }
        float mLastHitTime;
        [SerializeField] UnityEvent mHitedEvent;
        public void StartDelay()
        {
            mLastHitTime = Time.time;
        }
#if UNITY_EDITOR
        void Start()
        {
            Debug.Assert(GetComponents<Collider>().Any(x => x.isTrigger), $"Not found Trigger in {gameObject.name}");
            Debug.Assert(GetComponent<Rigidbody>().isKinematic);
        }
#endif

        void OnTriggerStay(Collider other)
        {
            if (!Flags.IsAttacker())
            {
                return;
            }

            HitBoxCollider victim = other.GetComponent<HitBoxCollider>();
            if (!victim)
            {
                return;
            }

            if (!Flags.CanAttack(victim))
            {
                return;
            }

            HitBoxCollider attacker = this;
            attacker.DoHit();
            victim.DoHit();

            attacker.HitedEventCallback?.Invoke(victim, attacker);
            victim.HitedEventCallback?.Invoke(victim, attacker);
        }
        void DoHit()
        {
            StartDelay();
            mHitedEvent.Invoke();
        }
    }
}