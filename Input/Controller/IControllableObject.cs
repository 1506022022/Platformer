using UnityEngine;

namespace RPG.Input.Controller
{
    public interface IControllableObject
    {
        public Rigidbody GetRigidbody();
        public Camera GetCamera();
        public float GetMoveSpeed();
        public bool IsControllable();
        public void OnBindControll();
        public void OnReleaseControll();
    }
}


