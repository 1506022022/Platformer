using RPG.Input.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG
{
    public class GameManager : MonoBehaviour
    {
        bool bGameStart;
        int mSceneLevel = 0;
        Controller mController;
        CharacterSwappingSystem mSwapSystem;

        [Header("로딩할 순서대로 씬 이름 입력")]
        [SerializeField] List<string> mLoadSceneNames;

        [Header("Components")]
        [SerializeField] Contents.Contents mContents;

        [Header("Options")]
        [SerializeField] bool mbCharacterSwap;
        [SerializeField] bool mbSingleState;
        List<ControllableCharacter> mControllableCharacters = new List<ControllableCharacter>();

        void StartGame()
        {
            Debug.Assert(mLoadSceneNames.Count > 0);

            bGameStart = false;
            string sceneName = mLoadSceneNames[mSceneLevel];
            mSceneLevel = Mathf.Min(mSceneLevel + 1, mLoadSceneNames.Count - 1);
            mContents.LoadScene(sceneName);

#if DEVELOPMENT
            var gms = FindObjectsOfType<GameManager>();
            Debug.Assert(gms.Count() == 1, $"GM is not Unique : {gms.Count()}");
#endif
        }
        void OnLoadedScene()
        {
            mControllableCharacters.Clear();
            foreach (var character in Character.Character.Instances)
            {
                var controllable = new ControllableCharacter(character);
                mControllableCharacters.Add(controllable);
            }
            SelectCharacterAndStartControll();
            mSwapSystem.BindCharacters(mControllableCharacters);
            bGameStart = true;
        }
        void OnClearGame()
        {
            if (!bGameStart)
            {
                return;
            }
            bGameStart = false;
            mController.IsActive = false;
            foreach (var character in mControllableCharacters)
            {
                mController.RemoveInputInteractionTarget(character);
            }
            StartCoroutine(GameEndProcess());
        }
        IEnumerator GameEndProcess()
        {
            yield return new WaitForSeconds(3.0f);
            StartGame();
        }
        void SelectCharacterAndStartControll()
        {
            Debug.Assert(mControllableCharacters.Count > 0, $"Cannot find character(Count : {mControllableCharacters.Count}).");
            var firstCharacter = mControllableCharacters.First();
            mController.AddInputInteractionTarget(firstCharacter);
            firstCharacter.OnBindControll();
            mController.IsActive = true;
        }
        void CreateController()
        {
            mController = new Controller();
            if (mbCharacterSwap)
            {
                mSwapSystem = new CharacterSwappingSystem(mController);
                mController.AddInputInteractionTarget(mSwapSystem);
            }
        }
        void BindContentsEvents()
        {
            mContents.AddListenerLoadedScene(OnLoadedScene);
            mContents.AddListenerClearGame(OnClearGame);
        }
        void SetContentsOption()
        {
            if (mbSingleState)
            {
                mContents.EnableLoadScene = false;
            }
        }
        void Awake()
        {
            CreateController();
            SetContentsOption();
            BindContentsEvents();
            DontDestroyOnLoad(gameObject);
        }
        void Start()
        {
            StartGame();
        }
        void Update()
        {
            if (!bGameStart) return;
            mController.Update();
        }
    }
}

