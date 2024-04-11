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

        static Dictionary<string, float> AxisRawList = new Dictionary<string, float>
        {
            { HORIZONTAL, 0f },
            { VERTICAL, 0f },
            { JUMP, 0f },
            { TAB, 0f }
        };
        public static Dictionary<string, float> GetAxisRawMap()
        {
            foreach (var button in AxisRawList.ToList())
            {
                AxisRawList[button.Key] = UnityEngine.Input.GetAxisRaw(button.Key);
            }
            return AxisRawList;
        }
    }
}

