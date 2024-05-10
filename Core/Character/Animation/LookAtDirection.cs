using UnityEngine;

public static class LookAtDirection
{
    static Vector3 m3DVelocity;

    public static void LookAtMovingDirection(Transform transform, Rigidbody rigid)
    {
        m3DVelocity = rigid.velocity;
        m3DVelocity.y = 0;
        if (m3DVelocity == Vector3.zero)
        {
            return;
        }

        transform.forward = m3DVelocity;
    }
}