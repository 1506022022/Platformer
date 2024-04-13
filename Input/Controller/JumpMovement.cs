using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static RPG.Input.ActionKey;
using static RPG.Input.Controller.MovementInfo;

namespace RPG.Input.Controller
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class JumpMovement : InputInteraction
    {
        public Func<bool> ConditionOfMoveable;
        float mJumpDelay;
        Vector3 mVelocity;
        Rigidbody mRigid;

        public override bool IsAble()
        {
            return ConditionOfMoveable?.Invoke() ?? true &&
                   RigidbodyUtil.IsGround(mRigid);
        }
        protected override void MappingInputEvent()
        {
            InputEventMap = new Dictionary<string, UnityAction<float>>()
            {
                { JUMP, Jump }
            };
        }
        protected override void Awake()
        {
            base.Awake();
            mRigid = GetComponent<Rigidbody>();
        }
        void Jump(float input)
        {
            if (!input.Equals(0) &&
                  mJumpDelay <= Time.time)
            {
                mRigid.AddForce(Vector3.up * JUMP_POWER);
                mJumpDelay = Time.time + JUMP_DELAY;
            }
            // 점프 속도 제한
            mVelocity = mRigid.velocity;
            mVelocity.y = Mathf.Clamp(mVelocity.y, -MAX_JUMP_VELOCITY, MAX_JUMP_VELOCITY);
            mRigid.velocity = mVelocity;
            //
        }
    }

}