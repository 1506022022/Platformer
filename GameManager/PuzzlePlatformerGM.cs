using RPG.Input.Controller;
using UnityEngine;

namespace RPG
{
    public class PuzzlePlatformerGM : GameManager
    {
        [SerializeField] protected  ControllerChanger mChanger;

        protected override void OnLoadedScene()
        {
            base.OnLoadedScene();
            mChanger.Controller = mPlayerController;
            mChanger.SetControllables(mPlayer);
        }
    }
}

