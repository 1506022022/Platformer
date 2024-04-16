namespace RPG.Character
{
    public interface ITransitionAnimation
    {
        public State UpdateAndGetState();
        public bool IsTransitionAbleState(State currentState);
    }
}