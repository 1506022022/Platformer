using RPG.Input.Controller;

namespace RPG
{
    public class PuzzlePlatformerGM : GameManager
    {
        CharacterSwappingSystem swappingController;

        protected override void OnLoadedScene()
        {
            base.OnLoadedScene();
            swappingController.SetControllables();
        }
        protected override void CreateController()
        {
            mPlayerController = new CharacterSwappingSystem();
            swappingController = mPlayerController as CharacterSwappingSystem;
        }
    }
}

