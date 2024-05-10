using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlatformGame.Input
{
    public static class ActionKey
    {
        public static readonly string UP = "Up";
        public static readonly string DOWN = "Down";
        public static readonly string RIGHT = "Right";
        public static readonly string LEFT = "Left";
        public static readonly string JUMP = "Jump";
        public static readonly string SWAP = "Tab";
        public static readonly string ATTACK = "Attack";
        public static readonly string GUARD = "Guard";
        public static readonly string PORTAL_JUMP = "PortalJump";

        static readonly Dictionary<string, bool> mKeyDownMap = new Dictionary<string, bool>
        {
            { UP, false },
            { DOWN, false },
            { RIGHT, false },
            { LEFT, false },
            { JUMP, false },
            { SWAP, false },
            { ATTACK, false },
            { GUARD, false },
            { PORTAL_JUMP, false }
        };

        static Dictionary<string, bool> KeyDownMap
        {
            get
            {
                Debug.Assert(mKeyDownMap != null);
                return mKeyDownMap;
            }
        }

        public static List<string> InputKeys => KeyDownMap.Keys.ToList();
        static float mLastUpdate;

        public static Dictionary<string, bool> GetKeyDownMap()
        {
            if (!(mLastUpdate < Time.time))
            {
                return mKeyDownMap;
            }

            foreach (var button in KeyDownMap.ToList())
            {
                mKeyDownMap[button.Key] = UnityEngine.Input.GetAxisRaw(button.Key) != 0;
            }

            mLastUpdate = Time.time;

            return mKeyDownMap;
        }
        
    }
}