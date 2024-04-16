using UnityEngine;
using static RPG.Character.AnimationTrigger;

namespace RPG.Character
{
    public class MovementAnimation : MonoBehaviour, ITransitionAnimation
    {
        float mMovement;
        string mTrigger;
        string mPrevTrigger;
        Vector2 m2DVelocity;
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
        public bool IsTransitionAbleState(State currentState)
        {
            return currentState == State.Idle ||
                   currentState == State.Running ||
                   currentState == State.Falling ||
                   currentState == State.Jumping;
        }
        public State UpdateAndGetState()
        {
            Debug.Assert(mAnimator && mRigid);
            mPrevTrigger = mTrigger;

            // GetState
            if (RigidbodyUtil.IsGround(mRigid))
            {
                mTrigger = GetMovementState();
                if (mPrevTrigger == FALL)
                {
                    mTrigger = IDLE;
                }
            }
            else
            {
                mTrigger = GetJumpState();
            }
            //

            //UpdateState

            foreach (var trigger in STATE_MAP)
            {
                if (trigger.Key == mTrigger)
                {
                    mAnimator.SetTrigger(mTrigger);
                }
                else
                {
                    mAnimator.ResetTrigger(trigger.Key);
                }

            }
            LookAtMovingDirection();
            //

            return STATE_MAP[mTrigger];
        }
        string GetMovementState()
        {
            m2DVelocity.x = mRigid.velocity.x;
            m2DVelocity.y = mRigid.velocity.z;
            mMovement = m2DVelocity.magnitude;

            return mMovement < 0.1f ? IDLE :
                   mMovement > 4.0f ? RUN :
                                      WALK;
        }
        string GetJumpState()
        {
            return mRigid.velocity.y > 0 ? JUMP :
                                           FALL;
        }
        void LookAtMovingDirection()
        {
            if (!(mTrigger == WALK || mTrigger == RUN)) return;
            m3DVelocity = mRigid.velocity;
            m3DVelocity.y = 0;
            mLookingDirection = Vector3.RotateTowards(mAnimator.transform.forward, m3DVelocity, 90, 90);
            mLookingDirection.z *= (mRigid.constraints & RigidbodyConstraints.FreezePositionZ) != 0 ? 1 : 0;
            if (mLookingDirection != Vector3.zero)
            {
                mAnimator.transform.rotation = Quaternion.LookRotation(mLookingDirection);
            }
        }
    }
}