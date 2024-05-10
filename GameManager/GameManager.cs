using PlatformGame.Character;
using PlatformGame.Character.Controller;
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
        public List<PlayerCharacter> JoinCharacters => JoinCharactersController.Select(x => x.PlayerCharacter).ToList();
        Contents.Contents mContents;
        PlayerCharacterController mController;
        [SerializeField] List<PlayerCharacterController> mJoinCharactersController;
        List<PlayerCharacterController> JoinCharactersController
        {
            get
            {
                Debug.Assert(mJoinCharactersController.Count > 0,$"{gameObject.name}에 컨트롤러가 할당되지 않음.");
                return mJoinCharactersController;
            }
            set
            {
                mJoinCharactersController = value;
            }
        }
        float mLastSwapTime;
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
            
            SelectControllCharacter();
            bGameStart = true;
        }

        public void SelectControllCharacter()
        {
            mController?.SetControll(false);
            mController = JoinCharactersController.First();
            mController.SetControll(true);
        }

        void SwapCharacter()
        {
            if(JoinCharactersController.Count < 2)
            {
                return;
            }
            var first = JoinCharactersController.First();
            JoinCharactersController.RemoveAt(0);
            JoinCharactersController.Add(first);
            SelectControllCharacter();
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
                // TODO : 분리
                if(Time.time < mLastSwapTime + 0.5f)
                {
                    return;
                }
                mLastSwapTime = Time.time;
                var map = ActionKey.GetAxisRawMap();
                if (!map[SWAP])
                {
                    return;
                }
                SwapCharacter();
                // TODOEND
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