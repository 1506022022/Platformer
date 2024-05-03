using Platformer;
using Platformer.Contents;
using UnityEngine;

namespace RPG.Contents
{
    public class Contents
    {
        static Contents mInstance;
        public static Contents Instance => mInstance;
        public AbilityState State => mLoader.State;
        ILevelLoader mLoader;

        public Contents(int loadType)
        {
            Debug.Assert(mInstance == null);
            mInstance = this;
            SetLoaderType(loadType);
        }
        public void LoadNextLevel()
        {
            mLoader.LoadNext();
        }
        public void SetLoaderType(int i)
        {
            if(i ==0 )
            {
                mLoader = new StageLoader();
            }
            else if( i == 1)
            {
                mLoader = GameObject.FindObjectOfType<CubeLoader>();
                Debug.Assert(mLoader != null);
            }
            else
            {
                mLoader = new LevelLoader();
            }
        }
        public void SetLoader(ILevelLoader loader)
        {
            mLoader = loader;
        }
    }
}