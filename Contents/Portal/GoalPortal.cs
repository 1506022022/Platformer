using PlatformGame.Contents.Loader;
using System.Linq;

namespace PlatformGame.Contents
{
    public class GoalPortal : Portal
    {
        public LoaderType LoaderType;

        protected override bool IsGoingLiveWithPortal(Character.Character other)
        {
            return mEnteredCharacters.All(x => x.Value) && State == WorkState.Ready;
        }

        protected override void RunPortal()
        {
            base.RunPortal();
            Contents.Instance.SetLoaderType(LoaderType);
            GameManager.Instance.LoadGame();
        }
    }
}