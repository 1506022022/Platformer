using System.Linq;
using UnityEngine;
using static RPG.Character.AnimationTrigger;

namespace RPG.Character
{
    public class CharacterAnimation
    {
        readonly Animator mAnimator;
        readonly Rigidbody mRigid;
        readonly BlendTrigger mLandTrigger;
        float mMovement;
        string mTrigger;
        string mPrevTrigger;
        Vector2 m2DVelocity;
        Vector3 m3DVelocity;
        Vector3 mLookingDirection;

        public CharacterAnimation(Animator animator, Rigidbody rigid)
        {
            mAnimator = animator;
            mRigid = rigid;
            mTrigger = IDLE;
            mLandTrigger = new BlendTrigger(LAND,
                              new string[] { WALK, RUN, IDLE });
        }
        public State UpdateAndGetState()
        {
            mPrevTrigger = mTrigger;

            // GetState
            if (RigidbodyUtil.IsGround(mRigid))
            {
                mTrigger = GetMovementState();
                if (mPrevTrigger == FALL)
                {
                    mTrigger = mLandTrigger.CheckORConditionsAndGetTrigger(mTrigger);
                }
            }
            else
            {
                mTrigger = GetJumpState();
            }
            //

            //UpdateState
            if (!mTrigger.Equals(mPrevTrigger))
            {
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
            }
            LookAtMovingDirection();
            //

            return STATE_MAP[mTrigger];
        }
        public bool IsGround()
        {
            Vector3 origin = mRigid.transform.position;
            origin.y += 0.5f;
            var hitAll = Physics.BoxCastAll(origin, Vector3.one * 0.25f, Vector3.down, mRigid.transform.rotation, 0.3f);
            var hitsExceptForMyself = hitAll.Where(x => !x.transform.Equals(mRigid.transform));

#if DEVELOPMENT
            if (hitsExceptForMyself.Any())
            {

                Debug.DrawRay(origin, Vector3.down, Color.yellow, hitsExceptForMyself.Min(x => x.distance));
                DebugGizmos.DrawWireCube(origin + Vector3.down * hitsExceptForMyself.Min(x => x.distance), Vector3.one * 0.25f, mRigid.GetHashCode());
            }
            else
            {
                Debug.DrawRay(origin, Vector3.down, Color.red, 0.3f);
                DebugGizmos.DrawWireCube(origin + Vector3.down * 0.3f, Vector3.one * 0.25f, mRigid.GetHashCode());
            }
#endif

            return hitsExceptForMyself.Any();
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
            if (mLookingDirection != Vector3.zero)
            {
                mAnimator.transform.rotation = Quaternion.LookRotation(mLookingDirection);
            }
        }
    }
}