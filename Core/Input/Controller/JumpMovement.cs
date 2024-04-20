using Platformer;
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
    public class JumpMovement : Ability
    {
        // Ability
        float mJumpDelay;
        Rigidbody mRigid;

        protected override void Awake()
        {
            base.Awake();
            mRigid = GetComponent<Rigidbody>();
        }
        void Jump()
        {
            mRigid.AddForce(Vector3.up * JUMP_POWER);
            mJumpDelay = Time.time + JUMP_DELAY;
            //// 점프 속도 제한
            //mVelocity = mRigid.velocity;
            //mVelocity.y = Mathf.Clamp(mVelocity.y, -MAX_JUMP_VELOCITY, MAX_JUMP_VELOCITY);
            //mRigid.velocity = mVelocity;
            ////
        }

        protected override void UpdateAbilityState()
        {
            if (Time.time < mJumpDelay)
            {
                mAbilityState = mAbilityState = AbilityState.Colltime;
            }
            else if (!RigidbodyUtil.IsGrounded(mRigid))
            {
                mAbilityState = mAbilityState = AbilityState.Action;
            }
            else
            {
                mAbilityState = mAbilityState = AbilityState.Ready;
            }
        }

        // IInputInteraction
        protected override void MappingInputEvent()
        {
            InputEventMap = new Dictionary<string, UnityAction<float>>()
            {
                { JUMP, (f) => Jump() }
            };
        }
    }

}