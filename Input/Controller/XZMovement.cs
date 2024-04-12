using RPG.Input.Controller;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static MovementInfo;
using static RPG.Input.ActionKey;

namespace RPG.Input
{
    public class XZMovement : InputInteraction
    {
        Vector3 mVertical;
        Vector3 mHorizontal;
        Vector3 mMoveDir;
        Vector3 mMoveVector;
        Vector3 mVelocity;
        Camera mObjectCam;
        Rigidbody mObjectRigid;
        IControllableObject mObject;
        public override bool IsAble => mObject != null &&
                              mObject.IsControllable();
        public void SetMovementObject(IControllableObject controlledTarget)
        {
            mObject = controlledTarget;
            mObjectRigid = mObject.GetRigidbody();
            mObjectCam = mObject.GetCamera();
        }
        protected override void MappingInputEvent()
        {
            InputEventMap = new Dictionary<string, UnityAction<float>>()
            {
                { VERTICAL, UpdateVerticalMovement },
                { HORIZONTAL, UpdateHorizontalMovement }
            };
        }
        void Move()
        {
            mMoveDir = mVertical + mHorizontal;

            // ����
            if (mMoveDir == Vector3.zero)
            {
                mObjectRigid.velocity *= 0.96f;
            }
            //
            else
            {
                mMoveVector = mMoveDir * mObject.GetMoveSpeed() * Time.deltaTime * 1000f;
                mObjectRigid.AddForce(mMoveVector);
            }

            // �̵� �ӵ� ����
            mVelocity = mObjectRigid.velocity;
            mVelocity.x = Mathf.Clamp(mVelocity.x, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mVelocity.z = Mathf.Clamp(mVelocity.z, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mObjectRigid.velocity = mVelocity;
            //
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
    }
}

