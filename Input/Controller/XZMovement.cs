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
            // TODO : Move를 동작시키기 위해서 추가함. 자연스럽지 않기 때문에 다른 방법을 모색.
            // 외부에서 호출해 주는 방식은 일단 보류
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

            // 감속
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

            // 이동 속도 제한
            mVelocity = mObjectRigid.velocity;
            mVelocity.x = Mathf.Clamp(mVelocity.x, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mVelocity.z = Mathf.Clamp(mVelocity.z, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mObjectRigid.velocity = mVelocity;
            //
        }
    }
}

