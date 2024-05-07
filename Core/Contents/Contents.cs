using PlatformGame.Contents.Loader;
using UnityEngine;

namespace PlatformGame.Contents
{
    public class Contents
    {
        public static Contents Instance { get; private set; }

        public WorkState State => mLoader.State;
        ILevelLoader mLoader;

        public Contents(int loadType)
        {
            Debug.Assert(Instance == null);
            Instance = this;
            SetLoaderType(loadType);
        }

        public void LoadNextLevel()
        {
            mLoader.LoadNext();
        }

        public void SetLoaderType(int i)
        {
            switch (i)
            {
                case 0:
                    mLoader = new StageLoader();
                    break;
                case 1:
                    mLoader = GameObject.FindObjectOfType<CubeLoader>();
                    Debug.Assert(mLoader != null);
                    break;
                default:
                    mLoader = new LevelLoader();
                    break;
            }
        }

        public void SetLoader(ILevelLoader loader)
        {
            mLoader = loader;
        }
    }
}