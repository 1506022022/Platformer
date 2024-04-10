using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.Contents
{
    public class Contents : MonoBehaviour
    {
        UnityEvent OnLoadedScene = new UnityEvent();
        UnityEvent OnClearGame = new UnityEvent();
        [SerializeField] Slider mProgressBar;
        [SerializeField] GameObject mLoadingCanvas;
        [SerializeField] TextMeshProUGUI mLoadSceneNameText;

        void Awake()
        {
            SceneLoader.CoroutineRunner = this;
            SceneLoader.LoadingCanvas = mLoadingCanvas;
            SceneLoader.ProgressBar = mProgressBar;
            SceneLoader.LoadSceneNameText = mLoadSceneNameText;
            SceneLoader.AddListenerLoadedSceneEvent(OnLoadedScene.Invoke);
            SceneLoader.AddListenerLoadedSceneEvent(ListeningMapClearEvent);
        }
        public void LoadScene(string sceneName)
        {
            SceneLoader.LoadScene(sceneName);
        }
        public void AddListenerLoadedScene(UnityAction action)
        {
            OnLoadedScene.AddListener(action);
        }
        public void AddListenerClearGame(UnityAction actoin)
        {
            OnClearGame.AddListener(actoin);
        }
        void ListeningMapClearEvent()
        {
            Map.Instance.AddListenerClearEvent(OnClearGame.Invoke);
        }
    }
}
