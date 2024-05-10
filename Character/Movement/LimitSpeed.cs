using UnityEngine;
using static PlatformGame.Character.Status.MovementInfo;

namespace PlatformGame.Character.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class LimitSpeed : MonoBehaviour
    {
        Rigidbody mRigid;
        void Awake()
        {
            mRigid = GetComponent<Rigidbody>();
        }

        void Update()
        {
            LimitMoveSpeed();
        }

        void LimitMoveSpeed()
        {
            var mVelocity = mRigid.velocity;
            mVelocity.x = Mathf.Clamp(mVelocity.x, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mVelocity.z = Mathf.Clamp(mVelocity.z, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mRigid.velocity = mVelocity;
        }

    }

}
