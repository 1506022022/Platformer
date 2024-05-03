using Platformer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Contents
{
    public abstract class Portal : MonoBehaviour
    {
        public AbilityState State { get; protected set; }
        protected Dictionary<Character.PlayerCharacter, bool> mGoalCheck = new Dictionary<Character.PlayerCharacter, bool>();
        [SerializeField] UnityEvent mRunEvent;

        void Start()
        {
            var playerList = Character.PlayerCharacter.Instances;
            foreach (var player in playerList)
            {
                mGoalCheck.Add(player, false);
            }
        }
        void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Character.PlayerCharacter>();
            if (player)
            {
                mGoalCheck[player] = true;
                if (mGoalCheck.All(x => x.Value) && State == AbilityState.Ready)
                {
                    RunPortal();
                }
            }
        }
        void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<Character.PlayerCharacter>();
            if(player)
            {
                if (mGoalCheck.ContainsKey(player))
                {
                    mGoalCheck[player] = false;
                }
            }
        }
        protected virtual void RunPortal()
        {
            State = AbilityState.Action;
            mRunEvent.Invoke();
        }
    }
}