using PlatformGame.Character.Combat;
using PlatformGame.Input;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame.Character.Controller
{
    [Serializable]
    public class AbilityKey
    {
        public string Key;
        public AbilityData Ability;
    }
    public class PlayerCharacterController : CharacterController
    {
        [HideInInspector] public List<AbilityKey> EditorInputMap;
        [HideInInspector] public PlayerCharacter BeforeTarget;
        Dictionary<string, AbilityData> mInputMap;
        public PlayerCharacter PlayerCharacter => Target as PlayerCharacter;

        public override void SetControll(bool able)
        {
            base.SetControll(able);
            FocusOn(able);
        }

        void FocusOn(bool on)
        {
            if (!PlayerCharacter.UI)
            {
                return;
            }

            PlayerCharacter.UI.SetActive(on);
        }

        void Awake()
        {
            mInputMap = new Dictionary<string, AbilityData>();
            foreach (var item in EditorInputMap)
            {
                Debug.Assert(!mInputMap.ContainsKey(item.Key), $"중복된 요소 : {item.Key}");
                mInputMap.Add(item.Key, item.Ability);
            }
        }

        void Update()
        {
            if (!IsAble)
            {
                return;
            }

            var map = ActionKey.GetAxisRawMap();

            foreach (var input_Ability in mInputMap)
            {
                var input = map[input_Ability.Key];
                if (input)
                {
                    var abilityID = input_Ability.Value.ID;
                    PlayerCharacter.CombatTo(abilityID);
                }
            }
        }
    }
}