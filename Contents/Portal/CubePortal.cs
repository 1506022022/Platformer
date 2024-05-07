using PlatformGame.Contents.Loader;
using System;
using UnityEngine;

namespace PlatformGame.Contents
{
    public class CubePortal : Portal
    {
        [SerializeField] CubeLoader mLoader;
        float mLastStop;

        protected override void RunPortal()
        {
            base.RunPortal();
            EnterCharacters();
            RunCube();
        }

        void Update()
        {
            switch (State)
            {
                case WorkState.Action:
                    {
                        if (IsCubeActionComplete())
                        {
                            StopPortal();
                        }

                        break;
                    }
                case WorkState.Cooltime:
                    {
                        if (mLastStop + 3f < Time.time)
                        {
                            State = WorkState.Ready;
                        }

                        break;
                    }
                case WorkState.Ready:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void StopPortal()
        {
            ExitsCharacters();
            State = WorkState.Cooltime;
            mLastStop = Time.time;
        }

        void RunCube()
        {
            Contents.Instance.SetLoader(mLoader);
            GameManager.Instance.LoadGame();
        }

        void EnterCharacters()
        {
            var characters = mGoalCheck.Keys;
            foreach (var character in characters)
            {
                character.gameObject.SetActive(false);
            }
        }

        void ExitsCharacters()
        {
            var characters = mGoalCheck.Keys;
            foreach (var character in characters)
            {
                character.gameObject.SetActive(true);
            }
        }

        bool IsCubeActionComplete()
        {
            return mLoader.State == WorkState.Cooltime;
        }
    }
}