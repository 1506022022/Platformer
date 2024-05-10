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

        void FixedUpdate()
        {
            LimitMoveSpeed();
        }

        void LimitMoveSpeed()
        {
            var currentVelocity = mRigid.velocity;
            var currentSpeed = currentVelocity.magnitude;
            
            if (currentSpeed <= MAX_MOVE_VELOCITY)
            {
                return;
            }
            
            var limitedVelocity = currentVelocity.normalized * MAX_MOVE_VELOCITY;
            mRigid.velocity = limitedVelocity;
        }
    }
}