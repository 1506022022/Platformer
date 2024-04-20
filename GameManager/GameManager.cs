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
        PlayerCharacterController mCharacterController;
        CharacterSwapController mSwapController;

        [Header("Components")]
        [SerializeField] Contents.Contents mContents;

        [Header("로딩할 순서대로 씬 이름 입력")]
        [SerializeField] List<string> mLoadSceneNames;

        [Header("Options")]
        [Tooltip("선택하면 로딩 없이 현재 씬을 플레이 할 수 있습니다.")]
        [SerializeField] bool mbSingleStage;

        void StartGame()
        {
            Debug.Assert(mLoadSceneNames.Count > 0);

            bGameStart = false;
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
            SelectControllCharacter();
            bGameStart = true;
        }
        void OnClearGame()
        {
            if (!bGameStart)
            {
                return;
            }
            bGameStart = false;
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
        void SelectControllCharacter()
        {
            var selectedCharacter = Character.Character.Instances[0];
            mCharacterController.SetControllCharacter(selectedCharacter);
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
        void SetControllers()
        {
            mCharacterController = new PlayerCharacterController();
            mSwapController = new CharacterSwapController(mCharacterController);
        }
        void Awake()
        {
            SetControllers();
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
            if(bGameStart)
            {
                mCharacterController.Update();
                mSwapController.Update();
            }
        }
    }
}

