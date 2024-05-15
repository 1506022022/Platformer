using PlatformGame.Character.Controller;
using PlatformGame.Contents;
using PlatformGame.Contents.Loader;
using PlatformGame.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using static PlatformGame.Input.ActionKey;

namespace PlatformGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public List<Character.Character> JoinCharacters => JoinCharactersController.Select(x => x.ControlledCharacter).ToList();
        Contents.Contents mContents;
        PlayerCharacterController mController;
        [SerializeField] List<PlayerCharacterController> mJoinCharactersController;

        List<PlayerCharacterController> JoinCharactersController
        {
            get
            {
                Debug.Assert(mJoinCharactersController.Count > 0 && mJoinCharactersController.All(x => x),
                    $"{gameObject.name}is not assigned a controller.");
                return mJoinCharactersController;
            }
            set => mJoinCharactersController = value;
        }

        float mLastSwapTime;

        [Header("[Debug]")]
        [SerializeField, ReadOnly(false)] LoaderType mLoaderType;
        [SerializeField, ReadOnly(false)] bool bGameStart;

        public void ExitGame()
        {
            Application.Quit();
        }

        public void LoadGame()
        {
            PauseGame();
            mContents.LoadNextLevel();
        }

        void StartGame()
        {
            bGameStart = true;
            ControlDefaultCharacter();
        }

        void PauseGame()
        {
            bGameStart = false;
            ReleaseController();
        }

        void ControlDefaultCharacter()
        {
            var defaultCharacter = JoinCharactersController.First();
            ReplaceControlWith(defaultCharacter);
        }

        void ReplaceControlWith(PlayerCharacterController controller)
        {
            mController?.SetActive(false);
            mController = controller;
            mController.SetActive(true);
        }

        void ReleaseController()
        {
            mController?.SetActive(false);
            mController = null;
        }

        void SwapCharacter()
        {
            if (JoinCharactersController.Count < 2)
            {
                return;
            }

            var first = JoinCharactersController.First();
            JoinCharactersController.RemoveAt(0);
            JoinCharactersController.Add(first);
            ControlDefaultCharacter();
        }

        void Awake()
        {
            Debug.Assert(Instance == null);
            Instance = this;
            DontDestroyOnLoad(gameObject);
            mContents = new Contents.Contents(mLoaderType);
        }

        void Start()
        {
            LoadGame();
        }

        void Update()
        {
            if (bGameStart)
            {
                // TODO : 분리
                if (Time.time < mLastSwapTime + 0.5f)
                {
                    return;
                }

                var map = ActionKey.GetKeyDownMap();
                if (!map[SWAP])
                {
                    return;
                }
                SwapCharacter();

                mLastSwapTime = Time.time;
                // TODOEND
            }
            else
            {
                if (mContents.State == WorkState.Ready)
                {
                    StartGame();
                }
            }
        }

    }
}