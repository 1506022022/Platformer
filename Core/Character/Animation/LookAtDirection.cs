using UnityEngine;

public static class LookAtDirection
{
    static Vector3 m3DVelocity;
    static Vector3 mLookingDirection;
    // TODO : ºÐ¸®
    public static void LookAtMovingDirection(Transform transform, Rigidbody rigid)
    {
        //if (!(mTrigger == WALK || mTrigger == RUN)) return;
        m3DVelocity = rigid.velocity;
        m3DVelocity.y = 0;
        mLookingDirection = Vector3.RotateTowards(rigid.transform.forward, m3DVelocity, 90, 90);
        //mLookingDirection.z *= (mRigid.constraints & RigidbodyConstraints.FreezePositionZ) != 0 ? 1 : 0;
        if (mLookingDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(mLookingDirection);
        }
    }
    // TODOEND
}
