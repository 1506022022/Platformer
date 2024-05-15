using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlatformGame.Input
{
    public static class ActionKey
    {
        public const string UP = "Up";
        public const string DOWN = "Down";
        public const string RIGHT = "Right";
        public const string LEFT = "Left";
        public const string JUMP = "Jump";
        public const string SWAP = "Tab";
        public const string ATTACK = "Attack";
        public const string GUARD = "Guard";
        public const string PORTAL_JUMP = "PortalJump";

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