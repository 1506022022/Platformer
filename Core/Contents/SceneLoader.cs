using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RPG.Contents
{
    public static class SceneLoader
    {
        static UnityEvent mOnLoadedScene = new UnityEvent();
        static GameObject mLoadingCanvas;
        public static GameObject LoadingCanvas
        {
            private get
            {
                Debug.Assert(mLoadingCanvas);
                return mLoadingCanvas;
            }
            set
            {
                mLoadingCanvas = value;
            }
        }
        static Slider mProgressBar;
        public static Slider ProgressBar
        {
            private get
            {
                Debug.Assert(mProgressBar);
                return mProgressBar;
            }
            set
            {
                mProgressBar = value;
            }
        }
        static TextMeshProUGUI mLoadSceneNameText;
        public static TextMeshProUGUI LoadSceneNameText
        {
            private get
            {
                Debug.Assert(mLoadSceneNameText);
                return mLoadSceneNameText;
            }
            set
            {
                mLoadSceneNameText = value;
            }
        }
        static MonoBehaviour mCoroutineRunner;
        public static MonoBehaviour CoroutineRunner
        {
            private get
            {
                Debug.Assert(mCoroutineRunner);
                return mCoroutineRunner;
            }
            set
            {
                mCoroutineRunner = value;
            }
        }
        public static void AddListenerLoadedSceneEvent(UnityAction action)
        {
            mOnLoadedScene.AddListener(action);
        }
        public static void LoadScene(string sceneName)
        {
            CoroutineRunner.StartCoroutine(LoadSceneProcess(sceneName));
        }
        static IEnumerator LoadSceneProcess(string sceneName)
        {
            LoadingCanvas.SetActive(true);
            LoadSceneNameText.text = sceneName;

            float timer = 0f;
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
                ProgressBar.normalizedValue = timer / 5f;
                yield return null;
            }
#else
            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress < 0.9f)
                {
                    ProgressBar.normalizedValue = asyncOperation.progress;
                }
                else
                {
                    timer += Time.unscaledDeltaTime;
                    ProgressBar.normalizedValue = Mathf.Lerp(0.9f, 1f, timer);
                    if (ProgressBar.normalizedValue >= 1f)
                    {
                        asyncOperation.allowSceneActivation = true;
                        break;
                    }
                }
                yield return null;
            }
#endif
            yield return new WaitForSeconds(0.2f);
            LoadingCanvas.SetActive(false);
            mOnLoadedScene.Invoke();
        }

    }
}
