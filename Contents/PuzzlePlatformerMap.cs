using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Contents
{
    public class PuzzlePlatformerMap : Map
    {
        Dictionary<GameObject, bool> mGoalCheck = new Dictionary<GameObject, bool>();
        [SerializeField] OnTriggerEventHandler mHandler;

        protected override void Start()
        {
            base.Awake();

            var playerList = GameObject.FindGameObjectsWithTag("Player");
            Debug.Assert(playerList.Count() > 0);
            foreach (var player in playerList)
            {
                mGoalCheck.Add(player, false);
            }

            Debug.Assert(mHandler);
            mHandler.AddListenerEnterEvent(OnTriggerEnterListener);
            mHandler.AddListenerExitEvent(OnTriggerExitListener);
        }
        void OnTriggerEnterListener(Collider other)
        {
            GameObject player = other.gameObject;
            if (player.tag.Equals("Player"))
            {
                mGoalCheck[player] = true;
                if (mGoalCheck.All(x => x.Value))
                {
                    Clear();
                }
            }
        }
        void OnTriggerExitListener(Collider other)
        {
            GameObject player = other.gameObject;
            if (mGoalCheck.ContainsKey(player))
            {
                mGoalCheck[player] = false;
            }
        }
    }
}
