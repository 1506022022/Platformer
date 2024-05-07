using UnityEngine;

namespace PlatformGame.Character.Controller
{
    public class CharacterController : MonoBehaviour
    {
        public bool IsAble
        {
            get; private set;
        }
        public Character Target;
        public virtual void SetControll(bool able)
        {
            IsAble = able;
        }
    }
}
