using UnityEngine;
using static RPG.Input.Controller.MovementInfo;

namespace RPG.Input.Controller
{
    public class XZMovement
    {
        float mMoveSpeed;
        Vector3 mVertical;
        Vector3 mHorizontal;
        Vector3 mMoveDir;
        Vector3 mMoveVector;
        Vector3 mVelocity;
        Camera mObjectCam;
        Rigidbody mObjectRigid;

        public XZMovement(Rigidbody rigid, Camera cam, float moveSpeed)
        {
            mObjectCam = cam;
            mObjectRigid = rigid;
            mMoveSpeed = moveSpeed;
        }
        public void UpdateHorizontalMovement(float input)
        {
            mHorizontal = mObjectCam.transform.right * input;
            // TODO : Move�� ���۽�Ű�� ���ؼ� �߰���. �ڿ������� �ʱ� ������ �ٸ� ����� ���.
            // �ܺο��� ȣ���� �ִ� ����� �ϴ� ����
            Move();
            // TODOEND
        }
        public void UpdateVerticalMovement(float input)
        {
            mVertical = mObjectCam.transform.forward * input;
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
                mMoveVector = mMoveDir * mMoveSpeed * Time.deltaTime * 1000f;
                mObjectRigid.AddForce(mMoveVector);
            }

            // �̵� �ӵ� ����
            mVelocity = mObjectRigid.velocity;
            mVelocity.x = Mathf.Clamp(mVelocity.x, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mVelocity.z = Mathf.Clamp(mVelocity.z, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mObjectRigid.velocity = mVelocity;
            //
        }
    }
}

