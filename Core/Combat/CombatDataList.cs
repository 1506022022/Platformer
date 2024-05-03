using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Core
{
    [CreateAssetMenu(fileName = "CombatDataList", menuName = "Custom/CombatDataList")]
    public class CombatDataList : ScriptableObject
    {
        [SerializeField]
        List<CombatData> mCombats;
        public List<CombatData> Combats
        {
            get
            {
                Debug.Assert(mCombats != null && mCombats.Count > 0);
                return mCombats;
            }
        }
        public Dictionary<uint,CombatData> Library
        {
            get
            {
                Dictionary<uint, CombatData> library = new Dictionary<uint, CombatData>();
                foreach(var item in mCombats)
                {
                    Debug.Assert(!library.ContainsKey(item.ID), $"중복된 값 : {item}");
                    library.Add(item.ID, item);
                }
                return library;
            }
        }
    }
}