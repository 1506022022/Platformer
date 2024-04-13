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
        CharacterSwappingSystem mSwapSystem;

        [Header("로딩할 순서대로 씬 이름 입력")]
        [SerializeField] List<string> mLoadSceneNames;

        [Header("Components")]
        [SerializeField] Controller mController;
        [SerializeField] Contents.Contents mContents;

        [Header("Options")]
        [Tooltip("선택하면 로딩 없이 현재 씬을 플레이 할 수 있습니다.")]
        [SerializeField] bool mbSingleStage;

        void StartGame()
        {
            Debug.Assert(mLoadSceneNames.Count > 0);

            bGameStart = false;
            mController.IsActive = false;
            string nextStage = mLoadSceneNames[mSceneLevel];
            mSceneLevel = Mathf.Min(mSceneLevel + 1, mLoadSceneNames.Count - 1);
            mContents.LoadScene(nextStage);

#if DEVELOPMENT
            var gms = FindObjectsOfType<GameManager>();
            Debug.Assert(gms.Count() == 1, $"GM is not Unique : {gms.Count()}");
#endif
        }
        void OnLoadedScene()
        {
            SelectCharacterAndStartControll();
            mSwapSystem?.BindCharacters(Character.Character.Instances);
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
            mSwapSystem?.ReleaseCharacters();
            StartCoroutine(GameEndProcess());
        }
        IEnumerator GameEndProcess()
        {
            yield return new WaitForSeconds(3.0f);
            if(!mbSingleStage)
            {
                StartGame();
            }
        }
        void SelectCharacterAndStartControll()
        {
            var selectedCharacter = Character.Character.Instances[0];
            selectedCharacter.FocusOn();
            var interactionTarget = IInputInteractionFactory
                                            .ConvertFromCharacter(selectedCharacter);
            mController.AddInputInteractionTargets(interactionTarget);
            mController.IsActive = true;
        }
        void BindContentsEvents()
        {
            mContents.AddListenerLoadedScene(OnLoadedScene);
            mContents.AddListenerClearGame(OnClearGame);
        }
        void SetContentsOption()
        {
            if (mbSingleStage)
            {
                mContents.EnableLoadScene = false;
            }
        }
        void Awake()
        {
            mSwapSystem = mController.GetComponent<CharacterSwappingSystem>();
            SetContentsOption();
            BindContentsEvents();
            DontDestroyOnLoad(gameObject);
        }
        void Start()
        {
            StartGame();
        }
    }
}

