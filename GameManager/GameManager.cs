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

        void StartGame()
        {
            Debug.Assert(mLoadSceneNames.Count > 0);

            bGameStart = false;
            string sceneName = mLoadSceneNames[sceneLevel];
            sceneLevel = Mathf.Min(sceneLevel + 1, mLoadSceneNames.Count - 1);
            mContents.LoadScene(sceneName);

#if DEVELOPMENT
            var gms = FindObjectsOfType<GameManager>();
            Debug.Assert(gms.Count() == 1);
#endif
        }
        void OnLoadedScene()
        {
            SelectCharacterAndStartControll();
            bGameStart = true;
        }
        void OnClearGame()
        {
            if (!bGameStart)
            {
                return;
            }
            bGameStart = false;
            mPlayerController.SetActive(false);
            StartCoroutine(GameEndProcess());
        }
        IEnumerator GameEndProcess()
        {
            yield return new WaitForSeconds(3.0f);
            StartGame();
        }
        void SelectCharacterAndStartControll()
        {
            mPlayer = FindObjectOfType<Character.Character>();
            Debug.Assert(mPlayer);
            CreateController();
            IControllableObject obj = ControllableFactory.CreateFromCharacter(mPlayer);
            var list = GetControllableList();
            mPlayerController.SetAllControlledTarget(list);
            mPlayerController.BindObject(obj);
            mPlayerController.SetActive(true);
        }
        void CreateController()
        {
            mPlayerController = new Controller();
        }
        List<IControllableObject> GetControllableList()
        {
            List<IControllableObject> list = new List<IControllableObject>();
            var characters = FindObjectsOfType<Character.Character>();
            foreach (var character in characters)
            {
                var obj = ControllableFactory.CreateFromCharacter(character);
                list.Add(obj);
            }
            return list;
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

