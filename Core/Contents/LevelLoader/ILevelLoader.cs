namespace Platformer.Contents
{
    public interface ILevelLoader
    {
        public void LoadNext();
        public AbilityState State { get; }
    }
}
