using PlatformGame.Character;
using PlatformGame.Contents.Loader;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Contents
{
    public abstract class Portal : MonoBehaviour
    {
        public WorkState State { get; protected set; }
        protected readonly Dictionary<PlayerCharacter, bool> mGoalCheck = new();
        [SerializeField] UnityEvent mRunEvent;

        void Start()
        {
            var needCharacters = GameManager.Instance.JoinCharacters;
            Debug.Assert(needCharacters.Count > 0);
            foreach (var player in needCharacters)
            {
                mGoalCheck.Add(player, false);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerCharacter>();
            if (!player)
            {
                return;
            }
            
            mGoalCheck[player] = true;
            if (mGoalCheck.All(x => x.Value) && State == WorkState.Ready)
            {
                RunPortal();
            }
        }

        void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<PlayerCharacter>();
            if (!player)
            {
                return;
            }
            
            if (mGoalCheck.ContainsKey(player))
            {
                mGoalCheck[player] = false;
            }
        }

        protected virtual void RunPortal()
        {
            State = WorkState.Action;
            mRunEvent.Invoke();
        }
        
    }
}