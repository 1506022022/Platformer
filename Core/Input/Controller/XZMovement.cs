using Platformer;
using UnityEngine;
using static RPG.Input.Controller.MovementInfo;

namespace RPG.Input.Controller
{
    public class XZMovement
    {
        Vector3 mMoveDir;
        Vector3 mMoveVector;
        Vector3 mVelocity;
        Rigidbody mRigid;
        Camera mObjectCam;
        public XZMovement(Rigidbody rigid, Camera cam)
        {
            mRigid = rigid;
            mObjectCam = cam;
        }
        public void Move(Vector3 dir)
        {
            Debug.Assert(dir.y == 0);
            mMoveDir = mObjectCam.transform.right * dir.x;
            mMoveDir += mObjectCam.transform.forward * dir.z;

            float jumpDisspeed = RigidbodyUtil.IsGrounded(mRigid) ? 1f : 0.2f;
            // 감속
            if (mMoveDir == Vector3.zero && jumpDisspeed == 1f)
            {
                //mRigid.velocity *= 0.96f;
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

