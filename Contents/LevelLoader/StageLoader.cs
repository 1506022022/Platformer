using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Platformer.Contents
{
    public class StageLoader : ILevelLoader
    {
        public AbilityState State { get; private set; }
        int mStageLevel;
        StageList Stages;
        Slider mProgressBar;
        TextMeshProUGUI mTitle;
        LoadingWindow mLoadingWindow;
        MonoBehaviour mCoroutineRunner;

        public StageLoader()
        {
            Stages = Resources.Load<StageList>("StageLevels");
            Debug.Assert(Stages);
            Debug.Assert(Stages.Names.Count > 0);

            mLoadingWindow = UIWindowContainer.GetLoadingWindow();
            mCoroutineRunner = mLoadingWindow.CoroutineRunner;
            mTitle = mLoadingWindow.LoadSceneNameText;
            mProgressBar = mLoadingWindow.ProgressBar;
        }
        public void LoadNext()
        {
            var sceneName = Stages.Names[mStageLevel];
            mStageLevel = Mathf.Min(mStageLevel + 1, Stages.Names.Count - 1);
            mLoadingWindow.ShowWindow(true);
            mCoroutineRunner.StartCoroutine(LoadSceneProcess(sceneName));
        }

        IEnumerator LoadSceneProcess(string sceneName)
        {
            mTitle.text = sceneName;
            float timer = 0f;
            State = AbilityState.Action;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
#if DEVELOPMENT
            while (true)
            {
                timer += Time.unscaledDeltaTime;
                if (timer > 5f)
                {
                    asyncOperation.allowSceneActivation = true;
                    break;
                }
                mProgressBar.normalizedValue = timer / 5f;

                yield return null;
            }
#else
            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress < 0.9f)
                {
                    mProgressBar.normalizedValue = asyncOperation.progress;
                }
                else
                {
                    timer += Time.unscaledDeltaTime;
                    mProgressBar.normalizedValue = Mathf.Lerp(0.9f, 1f, timer);
                    if (mProgressBar.normalizedValue >= 1f)
                    {
                        asyncOperation.allowSceneActivation = true;
                        break;
                    }
                }
                yield return null;
            }
#endif
            mLoadingWindow.ShowWindow(false);
            State = AbilityState.Ready;
        }
    }
}
