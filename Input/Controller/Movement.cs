using RPG.Input.Controller;
using UnityEngine;
using static UnityEngine.Input;

namespace RPG.Input
{
    public static class Movement
    {
        static Vector3 mVertical;
        static Vector3 mHorizontal;
        static Vector3 mMoveDir = Vector3.zero;
        static Camera mCam;

        public static Vector3 GetMoveDirection(IControllableObject obj)
        {
            mCam = obj.GetCamera();
            mVertical = mCam.transform.forward * GetAxisRaw("Vertical");
            mHorizontal = mCam.transform.right * GetAxisRaw("Horizontal");
            mMoveDir = mVertical + mHorizontal;
            return mMoveDir;
        }
        public static bool IsPressedJumpKey()
        {
            return GetAxisRaw("Jump") > 0;
        }
    }
}

