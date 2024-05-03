using System.Linq;
using UnityEngine;
using static RPG.Character.AnimationTrigger;

namespace RPG.Character
{
    public class MovementAnimation : MonoBehaviour
    {
        string mTrigger;
        Vector3 m3DVelocity;
        Vector3 mLookingDirection;
        [SerializeField] Rigidbody mRigid;
        [SerializeField] Animator mAnimator;

        void Awake()
        {
            Debug.Assert(mRigid);
            Debug.Assert(mAnimator);
            mTrigger = IDLE;
        }

        public void PlayAnimation(string clipName)
        {
            Debug.Assert(mAnimator.runtimeAnimatorController.animationClips.Any(x => x.name.Equals(clipName)), $"{clipName} is not found In Animator");
            mAnimator.Play(clipName);
        }

        public void UpdateAnimation(State state)
        {
            Debug.Assert(mAnimator && mRigid);
            mTrigger = STATE_MAP[state];

            foreach (var trigger in STATE_MAP.Values)
            {
                if (trigger == mTrigger)
                {
                    mAnimator.SetTrigger(mTrigger);
                }
                else
                {
                    mAnimator.ResetTrigger(trigger);
                }

            }
            LookAtMovingDirection();
        }

        void LookAtMovingDirection()
        {
            if (!(mTrigger == WALK || mTrigger == RUN)) return;
            m3DVelocity = mRigid.velocity;
            m3DVelocity.y = 0;
            mLookingDirection = Vector3.RotateTowards(mAnimator.transform.forward, m3DVelocity, 90, 90);
            //mLookingDirection.z *= (mRigid.constraints & RigidbodyConstraints.FreezePositionZ) != 0 ? 1 : 0;
            if (mLookingDirection != Vector3.zero)
            {
                mAnimator.transform.rotation = Quaternion.LookRotation(mLookingDirection);
            }
        }
    }
}