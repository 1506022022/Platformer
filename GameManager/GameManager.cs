using Platformer.Core;
using RPG.Character;
using System.ComponentModel;
using UnityEngine;
using AbilityState = Platformer.AbilityState;

namespace RPG
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        Contents.Contents mContents;
        PlayerCharacter character;

        [Header("[Debug]")]
        [SerializeField, ReadOnly(false)] bool bSingleStage;
        [SerializeField, ReadOnly(false)] bool bGameStart;

        public void LoadGame()
        {
            bGameStart = false;
            mContents.LoadNextLevel();
        }

        void OnLoadedScene()
        {
            SelectControllCharacter();
            bGameStart = true;
        }
        void SelectControllCharacter()
        {
            character = Character.PlayerCharacter.Instances[0];
            character.FocusOn();
        }
        void Awake()
        {
            Debug.Assert(Instance == null);
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // TODO : int -> enum º¯°æ
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
                PlayerCharacterController.ControllTo(character);
            }
            else
            {
                if (mContents.State == AbilityState.Ready)
                {
                    OnLoadedScene();
                }
            }
        }
    }
}