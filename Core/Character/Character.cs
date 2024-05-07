using PlatformGame.Character.Movement;
using PlatformGame.Tool;
using UnityEngine;

namespace PlatformGame.Character
{
    public abstract class Character : MonoBehaviour
    {
        [Header("[Character]")]
#if UNITY_EDITOR
        [SerializeField]
#endif
        CharacterState mState;
        public CharacterState State
        {
            get => mState;
            protected set => mState = value;
        }
        [SerializeField] protected Rigidbody mRigid;
        [SerializeField] protected CharacterMovement mMovement;

        protected void ReturnBasicState()
        {
            if (!RigidbodyUtil.IsGrounded(mRigid))
            {
                State = (mRigid.velocity.y >= 0) ? CharacterState.Jumping : CharacterState.Falling;
            }
            else
            {
                State = (Mathf.Abs(mRigid.velocity.magnitude) < 0.01f) ? CharacterState.Idle :
                    (mRigid.velocity.magnitude < 2f) ? CharacterState.Walk :
                    CharacterState.Running;
            }
        }

        protected virtual void Update()
        {
            ReturnBasicState();
        }

    }
}