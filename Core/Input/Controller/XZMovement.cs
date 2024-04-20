using Platformer;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static RPG.Input.ActionKey;
using static RPG.Input.Controller.MovementInfo;

namespace RPG.Input.Controller
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class XZMovement : Ability
    {
        Vector3 mVertical;
        Vector3 mHorizontal;
        Vector3 mMoveDir;
        Vector3 mMoveVector;
        Vector3 mVelocity;
        Rigidbody mRigid;
        [SerializeField] Camera mObjectCam;

        protected override void MappingInputEvent()
        {
            InputEventMap = new Dictionary<string, UnityAction<float>>()
            {
                { VERTICAL, UpdateVerticalMovement },
                { HORIZONTAL, UpdateHorizontalMovement }
            };
        }
        protected override void UpdateAbilityState()
        {
            mAbilityState = AbilityState.Ready;
        }
        protected override void Awake()
        {
            base.Awake();
            mRigid = GetComponent<Rigidbody>();
            Debug.Assert(mObjectCam && mRigid);
        }
        void UpdateHorizontalMovement(float input)
        {
            mHorizontal = transform.right * input;
            // TODO : Move를 동작시키기 위해서 추가함. 자연스럽지 않기 때문에 다른 방법을 모색.
            // 외부에서 호출해 주는 방식은 일단 보류
            Move();
            // TODOEND
        }
        void UpdateVerticalMovement(float input)
        {
            mVertical = transform.forward * input;
        }
        void Move()
        {
            mMoveDir = mVertical + mHorizontal;
            float jumpDisspeed = RigidbodyUtil.IsGrounded(mRigid) ? 1f : 0.2f;
            // 감속
            if (mMoveDir == Vector3.zero)
            {
                mRigid.velocity *= 0.86f;
            }
            //
            else
            {
                mMoveVector = mMoveDir * MOVE_SPEED * Time.deltaTime * 1000f * jumpDisspeed;
                mRigid.AddForce(mMoveVector);
            }

            // 이동 속도 제한
            mVelocity = mRigid.velocity;
            mVelocity.x = Mathf.Clamp(mVelocity.x, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mVelocity.z = Mathf.Clamp(mVelocity.z, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mRigid.velocity = mVelocity;
            //
        }

    }
}

