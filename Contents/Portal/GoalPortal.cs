namespace PlatformGame.Contents
{
    public class GoalPortal : Portal
    {
        public int LoaderType;
        protected override void RunPortal()
        {
            base.RunPortal();
            Contents.Instance.SetLoaderType(LoaderType);
            GameManager.Instance.LoadGame();
        }
    }
}