using UnityEngine;

namespace PlatformGame.Character.Movement
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody mRigid;
        public void PlayMovement(MovementAction action)
        {
            action.PlayAction(mRigid, this);
        }
    }
}