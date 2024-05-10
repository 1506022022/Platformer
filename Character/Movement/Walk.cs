using UnityEngine;
using static PlatformGame.Character.Status.MovementInfo;

namespace PlatformGame.Character.Movement
{
    [CreateAssetMenu(menuName = "Custom/MovementAction/XZMovement")]
    public class Walk : MovementAction
    {
        public Vector3 mDir;

        public override void PlayAction(Rigidbody rigid, MonoBehaviour coroutine)
        {
            Debug.Assert(Camera.main);
            Debug.Assert(mDir.y == 0);
            var camTransform = Camera.main.transform;
            Debug.Assert(camTransform);
            var moveForce = camTransform.right * mDir.x;
            moveForce += camTransform.forward * mDir.z;
            moveForce = moveForce.normalized * (Time.deltaTime * MOVE_SPEED);
            rigid.AddForce(moveForce);
        }
        
    }
}

