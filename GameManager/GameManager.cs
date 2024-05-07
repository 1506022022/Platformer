using PlatformGame.Character;
using PlatformGame.Character.Controller;
using PlatformGame.Contents.Loader;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace PlatformGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public List<PlayerCharacter> JoinCharacters => JoinCharactersController.Select(x => x.PlayerCharacter).ToList();
        Contents.Contents mContents;
        PlayerCharacterController mController;
        [SerializeField] List<PlayerCharacterController> JoinCharactersController;

        [Header("[Debug]")]
        [SerializeField, ReadOnly(false)] bool bSingleStage;
        [SerializeField, ReadOnly(false)] bool bGameStart;

        public void LoadGame()
        {
            bGameStart = false;
            ReleaseController();
            mContents.LoadNextLevel();
        }

        void OnLoadedScene()
        {
            Debug.Assert(JoinCharactersController.Count > 0);
            SelectControllCharacter();
            bGameStart = true;
        }

        void SelectControllCharacter()
        {
            Debug.Assert(JoinCharactersController.Count > 0);
            mController?.SetControll(false);
            mController = JoinCharactersController[0];
            mController.SetControll(true);
        }

        void ReleaseController()
        {
            mController?.SetControll(false);
            mController = null;
        }

        void Awake()
        {
            Debug.Assert(Instance == null);
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // TODO : int -> enum
            int loadType = bSingleStage ? 2 : 0;
            mContents = new Contents.Contents(loadType);
            // TODOEND
        }

        void Start()
        {
            LoadGame();
        }

        void Update()
        {
            if (bGameStart)
            {
                // PlayerCharacterController.ControllTo(character);
            }
            else
            {
                if (mContents.State == WorkState.Ready)
                {
                    OnLoadedScene();
                }
            }
        }

    }
}