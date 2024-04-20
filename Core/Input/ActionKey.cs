using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Input
{
    public static class ActionKey
    {
        public static readonly string HORIZONTAL = "Horizontal";
        public static readonly string VERTICAL = "Vertical";
        public static readonly string JUMP = "Jump";
        public static readonly string SWAP = "Tab";
        public static readonly string ATTACK = "Attack";
        public static readonly string GUARD = "Guard";

        static Dictionary<string, float> mAxisRawMap = new Dictionary<string, float>
        {
            { HORIZONTAL,   0f },
            { VERTICAL,     0f },
            { JUMP,         0f },
            { SWAP,         0f },
            { ATTACK,       0f },
            { GUARD,        0f }
        };

        public static float lastUpdate;
        public static Dictionary<string, float> GetAxisRawMap()
        {
            if (lastUpdate < Time.time)
            {
                foreach (var button in mAxisRawMap.ToList())
                {
                    mAxisRawMap[button.Key] = UnityEngine.Input.GetAxisRaw(button.Key);
                }
                lastUpdate = Time.time;
            }

            // TODO : 참조 복사본 반환해야 함
            return mAxisRawMap;
            // TODOEND
        }
    }
}

