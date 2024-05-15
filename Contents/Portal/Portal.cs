using PlatformGame.Contents.Loader;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Contents
{
    public abstract class Portal : MonoBehaviour
    {
        protected WorkState State { get; set; }
        protected readonly Dictionary<Character.Character, bool> mEnteredCharacters = new();
        [SerializeField] UnityEvent mRunEvent;

        protected virtual void RunPortal()
        {
            State = WorkState.Action;
            mRunEvent.Invoke();
        }

        protected abstract bool IsGoingLiveWithPortal(Character.Character other);

        void ResetEnteredCharacterMap()
        {
            mEnteredCharacters.Clear();
            var characters = GameManager.Instance.JoinCharacters;
            foreach (var character in characters)
            {
                mEnteredCharacters.Add(character, false);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            var character = other.GetComponent<Character.Character>();
            if (!character)
            {
                return;
            }
            mEnteredCharacters[character] = true;

            if (!IsGoingLiveWithPortal(character))
            {
                return;
            }
            RunPortal();
        }

        void OnTriggerExit(Collider other)
        {
            var character = other.GetComponent<Character.Character>();
            if (!character)
            {
                return;
            }

            if (mEnteredCharacters.ContainsKey(character))
            {
                mEnteredCharacters[character] = false;
            }
        }

        void Start()
        {
            ResetEnteredCharacterMap();
        }

    }
}