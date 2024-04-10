using RPG.Input.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG
{
    public class GameManager : MonoBehaviour
    {
        protected bool bGameStart;
        protected Controller mPlayerController;
        protected Character.Character mPlayer;
        [Header("로딩할 순서대로 씬 이름 입력")]
        [SerializeField] protected List<string> mLoadSceneNames;
        [Header("Components")]
        [SerializeField] protected Contents.Contents mContents;
        int sceneLevel = 0;

        protected virtual void StartGame()
        {
            Debug.Assert(mLoadSceneNames.Count > 0);

            bGameStart = false;
            string sceneName = mLoadSceneNames[sceneLevel];
            sceneLevel = Mathf.Min(sceneLevel + 1, mLoadSceneNames.Count-1);
            mContents.LoadScene(sceneName);

#if DEVELOPMENT
            var gms = FindObjectsOfType<GameManager>();
            Debug.Assert(gms.Count() == 1);
#endif
        }
        protected virtual void OnLoadedScene()
        {
            SelectCharacterAndStartControll();
            bGameStart = true;
        }
        protected virtual void OnClearGame()
        {
            bGameStart = false;
            mPlayerController.SetActive(false);
            StartCoroutine(GameEndProcess());
        }
        protected virtual IEnumerator GameEndProcess()
        {
            yield return new WaitForSeconds(3.0f);
            StartGame();
        }
        void SelectCharacterAndStartControll()
        {
            mPlayer = FindObjectOfType<Character.Character>();
            Debug.Assert(mPlayer);
            mPlayerController = new Controller();
            mPlayerController.SetControlledTarget(mPlayer);
            mPlayerController.SetActive(true);
        }
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            mContents.AddListenerLoadedScene(OnLoadedScene);
            mContents.AddListenerClearGame(OnClearGame);
        }
        void Start()
        {
            StartGame();
        }
        void Update()
        {
            if (!bGameStart) return;
            mPlayerController.Update();
        }
    }
}

