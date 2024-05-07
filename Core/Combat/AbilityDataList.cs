using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(fileName = "CombatDataList", menuName = "Custom/CombatDataList")]
    public class AbilityDataList : ScriptableObject
    {
        [SerializeField]
        List<AbilityData> mCombats;
        public List<AbilityData> Combats
        {
            get
            {
                Debug.Assert(mCombats != null && mCombats.Count > 0);
                return mCombats;
            }
        }
        public Dictionary<uint, AbilityData> Library
        {
            get
            {
                Dictionary<uint, AbilityData> library = new Dictionary<uint, AbilityData>();
                foreach (var item in mCombats)
                {
                    Debug.Assert(!library.ContainsKey(item.ID), $"중복된 값 : {item}");
                    library.Add(item.ID, item);
                }
                return library;
            }
        }
    }
}