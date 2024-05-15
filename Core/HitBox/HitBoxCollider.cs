using PlatformGame.Pipeline;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Character.Collision
{
    public delegate void HitEvent(CollisionData collision);

    public struct CollisionData
    {
        public Character Victim;
        public Character Attacker;
        public HitBoxCollider Subject;
    }

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class HitBoxCollider : MonoBehaviour
    {
        public float HitDelay;
        [SerializeField] HitBoxFlag mHitBoxFlag;
        public HitBoxFlag HitBoxFlag => mHitBoxFlag;
        public Character Actor;

        public bool IsDelay => Time.time < mLastHitTime + HitDelay;

        float mLastHitTime;
        [SerializeField] UnityEvent<CollisionData> mFixedHitEvent;
        HitEvent mAttackEvent;
        HitEvent mHitCallback;
        Pipeline<CollisionData> mHitPipeline;

        public void AddCallback(HitEvent callback)
        {
            mHitCallback += callback;
        }

        public void SetHitEvent(HitEvent hitEvent)
        {
            mAttackEvent = hitEvent;
        }

        public void StartDelay()
        {
            mLastHitTime = Time.time;
        }

        void InvokeHitCallback(CollisionData collision)
        {
            mHitCallback?.Invoke(collision);
        }

        void InvokeAttackEvent(CollisionData collision)
        {
            mAttackEvent?.Invoke(collision);
        }

        void InvokeFixedHitEvent(CollisionData collision)
        {
            mFixedHitEvent.Invoke(collision);
        }

        void DoHit(CollisionData collision)
        {
            StartDelay();
            mHitPipeline.Invoke(collision);
        }

        void OnTriggerStay(Collider other)
        {
            if (!HitBoxFlag.IsAttacker())
            {
                return;
            }

            var victim = other.GetComponent<HitBoxCollider>();
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

            var attacker = this;
            var collsion = new CollisionData()
            {
                Attacker = attacker.Actor,
                Victim = victim.Actor,
                Subject = this
            };

            attacker.DoHit(collsion);
            victim.DoHit(collsion);
        }

        void Start()
        {
            Debug.Assert(Actor, $"Actor not found : {gameObject.name}");
            Debug.Assert(GetComponents<Collider>().Any(x => x.isTrigger), $"Trigger not found : {gameObject.name}");
            Debug.Assert(GetComponent<Rigidbody>().isKinematic, $"Not set Kinematic : {gameObject.name}");
        }

        void Awake()
        {
            mLastHitTime = Time.time - HitDelay + 0.1f;

            mHitPipeline = Pipelines.Instance.HitBoxColliderPipeline;
            mHitPipeline.InsertPipe(InvokeAttackEvent);
            mHitPipeline.InsertPipe(InvokeFixedHitEvent);
            mHitPipeline.InsertPipe(InvokeHitCallback);
        }
    }
}