using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Character.Collision
{
    public delegate void HitBoxColliderCallback(HitBoxCollider subject);
    public delegate void HitEvent(CollisionData collision);
    public struct CollisionData
    {
        public Character Victim;
        public Character Attacker;
    }

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class HitBoxCollider : MonoBehaviour
    {
        static readonly int LOG_LAYER = 0;
        static readonly int LAYER_EVENT = 1;
        static readonly int LAYER_EVENT_FIXED = 2;
        public float HitDelay;
        public Character Actor;
        public HitBoxFlag HitBoxFlag;
        public HitBoxColliderCallback HitCallback;
        public bool IsDelay
        {
            get => Time.time < mLastHitTime + HitDelay;
        }
        float mLastHitTime;
        HitEventPipeline mHitPipeline = new HitEventPipeline(3);
        [SerializeField] UnityEvent<CollisionData> mFixedHitEvent;

        public void SetHitEvnet(HitEvent hitEvent)
        {
            mHitPipeline.RemoveAllProcessTo(LAYER_EVENT);
            mHitPipeline.AddProcessTo(LAYER_EVENT, hitEvent);
        }

        public void StartDelay()
        {
            mLastHitTime = Time.time;
        }

        void OnTriggerStay(Collider other)
        {
            if (!HitBoxFlag.IsAttacker())
            {
                return;
            }

            HitBoxCollider victim = other.GetComponent<HitBoxCollider>();
            if (!victim)
            {
                return;
            }
            if (victim.Actor.Equals(Actor))
            {
                return;
            }

            if (!HitBoxFlag.CanAttack(victim))
            {
                return;
            }

            HitBoxCollider attacker = this;
            CollisionData collsion = new CollisionData()
            {
                Attacker = attacker.Actor,
                Victim = victim.Actor
            };

            attacker.DoHit(collsion);
            victim.DoHit(collsion);
        }

        void DoHit(CollisionData collision)
        {
            StartDelay();
            HitCallback?.Invoke(this);
            mHitPipeline.Invoke(collision);
        }

#if UNITY_EDITOR
        void Start()
        {
            string hitLog = HitBoxFlag.IsAttacker() ? "공격" : "피격";
            // mHitPipeline.AddProcessTo(LOG_LAYER, (collision) => { Debug.Log($"{Actor.name}가 {hitLog}. {collision.Attacker.name}->{collision.Victim.name}."); });
            Debug.Assert(GetComponents<Collider>().Any(x => x.isTrigger), $"Not found Trigger in {gameObject.name}");
            Debug.Assert(GetComponent<Rigidbody>().isKinematic, $"Object : {gameObject.name}");
            Debug.Assert(Actor, $"Object : {gameObject.name}");
        }
#endif
        void Awake()
        {
            mLastHitTime = Time.time + 0.5f;
            mHitPipeline.AddProcessTo(LAYER_EVENT_FIXED, (collision) => mFixedHitEvent.Invoke(collision));
        }

    }
}