using UnityEngine;

namespace PlatformGame.Character.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementComponent : MonoBehaviour
    {
        Rigidbody mRigid;
        MovementAction mBeforeAction;
        [SerializeField] MovementAction mDefaultActionOrNull;

        public void PlayMovement(MovementAction action)
        {
            RemoveMovement();
            mBeforeAction = action;
            action.PlayAction(mRigid, this);
        }

        public void RemoveMovement()
        {
            StopAllCoroutines();
            mBeforeAction?.StopAction(mRigid, this);
        }

        void Awake()
        {
            mRigid = GetComponent<Rigidbody>();
            if (mDefaultActionOrNull != null)
            {
                PlayMovement(mDefaultActionOrNull);
            }
        }

    }
}