using UnityEngine;
using static PlatformGame.Character.Status.MovementInfo;

namespace PlatformGame.Character.Movement
{
    [CreateAssetMenu(menuName = "Custom/MovementAction/XZMovement")]
    public class Walk : MovementAction
    {
        public Vector3 mDir;
        Vector3 mMoveForce;
        Transform mCamTransform;

        public override void PlayAction(Rigidbody rigid, MonoBehaviour coroutine)
        {
            mCamTransform = GetCamera();
            Debug.Assert(mDir.y == 0);
            Debug.Assert(mCamTransform);
            mMoveForce = mCamTransform.right * mDir.x;
            mMoveForce += mCamTransform.forward * mDir.z;
            mMoveForce = mMoveForce * Time.deltaTime * MOVE_SPEED;
            rigid.AddForce(mMoveForce);
        }

        Transform GetCamera()
        {
            return Camera.main.transform;
        }
    }
}

