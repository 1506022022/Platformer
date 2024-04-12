using UnityEngine;

namespace RPG.Character
{
    public interface ITransitionAnimation
    {
        public State UpdateAndGetState();
        public bool IsTransitionAbleState(State currentState);
        public void SetAnimationTarget(Animator animator, Rigidbody rigid);
    }
}