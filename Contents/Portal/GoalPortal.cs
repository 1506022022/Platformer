using RPG;
using RPG.Contents;

namespace Platformer
{
    public class GoalPortal : Portal
    {
        protected override void RunPortal()
        {
            base.RunPortal();
            GameManager.Instance.LoadGame();
        }
    }
}