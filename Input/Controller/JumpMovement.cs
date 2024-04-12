using UnityEngine;
using static RPG.Input.Controller.MovementInfo;

namespace RPG.Input.Controller
{
    public class JumpMovement
    {
        float mJumpDelay;
        Vector3 mVelocity;
        Rigidbody mObjectRigid;
        public JumpMovement(Rigidbody rigid)
        {
            mObjectRigid = rigid;
        }
        public void Jump(float input)
        {
            if (input.Equals(0f))
            {
                return;
            }
            if (mJumpDelay <= Time.time)
            {
                mObjectRigid.AddForce(Vector3.up * JUMP_POWER);
                mJumpDelay = Time.time + JUMP_DELAY;
            }
            // ���� �ӵ� ����
            mVelocity = mObjectRigid.velocity;
            mVelocity.y = Mathf.Clamp(mVelocity.y, -MAX_JUMP_VELOCITY, MAX_JUMP_VELOCITY);
            mObjectRigid.velocity = mVelocity;
            //
        }
    }

}