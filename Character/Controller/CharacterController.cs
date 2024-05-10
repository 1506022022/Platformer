using UnityEngine;

namespace PlatformGame.Character.Controller
{
    public class CharacterController : MonoBehaviour
    {
        public bool IsActive
        {
            get; private set;
        }
        public Character Target;

        public virtual void SetActive(bool able)
        {
            IsActive = able;
        }
        
    }
}