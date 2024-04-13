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
    public class XZMovement : InputInteraction
    {
        float mMoveSpeed = MOVE_SPEED;
        Vector3 mVertical;
        Vector3 mHorizontal;
        Vector3 mMoveDir;
        Vector3 mMoveVector;
        Vector3 mVelocity;
        Rigidbody mRigid;
        [SerializeField] Camera mObjectCam;
        public Func<bool> ConditionOfMoveable;

        public override bool IsAble()
        {
            return ConditionOfMoveable?.Invoke() ?? true &&
                   RigidbodyUtil.IsGround(mRigid);
        }
        protected override void MappingInputEvent()
        {
            InputEventMap = new Dictionary<string, UnityAction<float>>()
            {
                { VERTICAL, UpdateVerticalMovement },
                { HORIZONTAL, UpdateHorizontalMovement }
            };

        }
        protected override void Awake()
        {
            Debug.Assert(mObjectCam);
            base.Awake();
            mRigid = GetComponent<Rigidbody>();
        }
        void UpdateHorizontalMovement(float input)
        {
            mHorizontal = mObjectCam.transform.right * input;
            // TODO : Move�� ���۽�Ű�� ���ؼ� �߰���. �ڿ������� �ʱ� ������ �ٸ� ����� ���.
            // �ܺο��� ȣ���� �ִ� ����� �ϴ� ����
            Move();
            // TODOEND
        }
        void UpdateVerticalMovement(float input)
        {
            mVertical = mObjectCam.transform.forward * input;
        }
        void Move()
        {
            mMoveDir = mVertical + mHorizontal;

            // ����
            if (mMoveDir == Vector3.zero)
            {
                mRigid.velocity *= 0.96f;
            }
            //
            else
            {
                mMoveVector = mMoveDir * mMoveSpeed * Time.deltaTime * 1000f;
                mRigid.AddForce(mMoveVector);
            }

            // �̵� �ӵ� ����
            mVelocity = mRigid.velocity;
            mVelocity.x = Mathf.Clamp(mVelocity.x, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mVelocity.z = Mathf.Clamp(mVelocity.z, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mRigid.velocity = mVelocity;
            //
        }
    }
}

