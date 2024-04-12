using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Contents
{
    public class PortalStage : Stage
    {
        Dictionary<GameObject, bool> mGoalCheck = new Dictionary<GameObject, bool>();
        void Awake()
        {
            // TODO : 플레이어 읽어오는 방식 변경
            var playerList = GameObject.FindGameObjectsWithTag("Player");
            // TODOEND
            Debug.Assert(playerList.Count() > 0);
            foreach (var player in playerList)
            {
                mGoalCheck.Add(player, false);
            }
        }
        void OnTriggerEnter(Collider other)
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
        void OnTriggerExit(Collider other)
        {
            GameObject player = other.gameObject;
            if (mGoalCheck.ContainsKey(player))
            {
                mGoalCheck[player] = false;
            }
        }
    }
}