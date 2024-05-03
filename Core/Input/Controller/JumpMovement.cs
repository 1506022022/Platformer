using UnityEngine;
using static RPG.Input.Controller.MovementInfo;

namespace RPG.Input.Controller
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class JumpMovement
    {
        float mJumpDelay;
        Rigidbody mRigid;
        public bool IsDelay => Time.time < mJumpDelay;

        public JumpMovement(Rigidbody rigid)
        {
            mRigid = rigid;
        }

        public void Jump()
        {
            if(Time.time < mJumpDelay)
            {
                return;
            }
            mRigid.AddForce(Vector3.up * JUMP_POWER);
            mJumpDelay = Time.time + JUMP_DELAY;
        }

    }
}