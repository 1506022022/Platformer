using System.Collections.Generic;
using System.Linq;

namespace RPG.Input
{
    public static class ActionKey
    {
        public static readonly string HORIZONTAL = "Horizontal";
        public static readonly string VERTICAL = "Vertical";
        public static readonly string JUMP = "Jump";
        public static readonly string TAB = "Tab";
        public static readonly string ATTACK = "Attack";
        public static readonly string GUARD = "Guard";

        static Dictionary<string, float> mAxisRawMap = new Dictionary<string, float>
        {
            { HORIZONTAL,   0f },
            { VERTICAL,     0f },
            { JUMP,         0f },
            { TAB,          0f },
            { ATTACK,       0f },
            { GUARD,        0f }
        };
        public static Dictionary<string, float> GetAxisRawMap()
        {
            // TODO : 참조 복사본 반환
            foreach (var button in mAxisRawMap.ToList())
            {
                mAxisRawMap[button.Key] = UnityEngine.Input.GetAxisRaw(button.Key);
            }
            // TODOEND

            return mAxisRawMap;
        }
    }
}

