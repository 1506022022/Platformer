using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static MovementInfo;
using static RPG.Input.ActionKey;

namespace RPG.Input.Controller
{
    public class JumpMovement : InputInteraction
    {
        public override bool IsAble => mObject != null &&
                              mObject.IsControllable();
        float mJumpDelay;
        Vector3 mVelocity;
        Rigidbody mObjectRigid;
        IControllableObject mObject;
        public void SetMovementObject(IControllableObject controlledTarget)
        {
            mObject = controlledTarget;
            mObjectRigid = mObject.GetRigidbody();
        }
        protected override void MappingInputEvent()
        {
            InputEventMap = new Dictionary<string, UnityAction<float>>()
            {
                { JUMP, Jump }
            };
        }
        void Jump(float input)
        {
            if (input.Equals(0f))
            {
                return;
            }
            if (mJumpDelay <= Time.time)
            {
                mObjectRigid.AddForce(Vector3.up * JUMP_POWER);
                mJumpDelay = Time.time + JUMP_Delay;
            }
            // 점프 속도 제한
            mVelocity = mObjectRigid.velocity;
            mVelocity.y = Mathf.Clamp(mVelocity.y, -MAX_JUMP_VELOCITY, MAX_JUMP_VELOCITY);
            mObjectRigid.velocity = mVelocity;
            //
        }
    }

}