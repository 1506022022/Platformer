using UnityEngine;
using static MovementInfo;

namespace RPG.Input.Controller
{
    public class Controller
    {
        public bool Active
        {
            get
            {
                return mbActive;
            }
        }
        bool mbActive;
        protected IControllableObject mControlledTarget;
        float mJumpDelay;
        Vector3 mMoveDir;
        Vector3 mMoveVector;
        Vector3 mVelocity;
        Rigidbody mTargetRigid;

        public virtual void Update()
        {
            if (!Active) return;
            Debug.Assert(mTargetRigid);
            if (!mControlledTarget.IsControllable()) return;
            DoJumpIfPressed();
            DoMoveIfPressed();

            // TODO : �����Է�, ��ȣ�ۿ��Է� ��� ����
            ActionInput.IsAttacking();
            ActionInput.IsInteraction();
            // TODOEND
        }
        public void SetActive(bool active)
        {
            mbActive = active;
        }
        public void SetControlledTarget(IControllableObject target)
        {
            mControlledTarget = target;
            mTargetRigid = target.GetRigidbody();
            target.SetControlledTarget();
        }
        void DoMoveIfPressed()
        {
            mMoveDir = Input.Movement.GetMoveDirection(mControlledTarget);
            mMoveVector = mMoveDir * mControlledTarget.GetMoveSpeed() * Time.deltaTime * 1000f;
            mTargetRigid.AddForce(mMoveVector);
            // ����
            if (mMoveDir == Vector3.zero)
            {
                mTargetRigid.velocity *= 0.96f;
            }
            //

            // �̵� �ӵ� ����
            mVelocity = mTargetRigid.velocity;
            mVelocity.x = Mathf.Clamp(mVelocity.x, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mVelocity.z = Mathf.Clamp(mVelocity.z, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mTargetRigid.velocity = mVelocity;
            //
        }
        void DoJumpIfPressed()
        {
            if (Movement.IsPressedJumpKey() &&
                mJumpDelay <= Time.time)
            {
                mTargetRigid.AddForce(Vector3.up * JUMP_POWER);
                mJumpDelay = Time.time + JUMP_Delay;
            }
            // ���� �ӵ� ����
            mVelocity = mTargetRigid.velocity;
            mVelocity.y = Mathf.Clamp(mVelocity.y, -MAX_JUMP_VELOCITY, MAX_JUMP_VELOCITY);
            mTargetRigid.velocity = mVelocity;
            //
        }
    }
}
