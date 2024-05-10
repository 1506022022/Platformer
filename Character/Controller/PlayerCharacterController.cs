using PlatformGame.Character.Combat;
using PlatformGame.Input;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        public UnityEvent<string, AbilityData> KeyInputEvent;

        public override void SetControll(bool able)
        {
            base.SetControll(able);
            FocusOn(able);
        }

        public void SetAbilityKeys(List<AbilityKey> abilityKeys)
        {
            mInputMap = new Dictionary<string, AbilityData>();
            foreach (var item in abilityKeys)
            {
                Debug.Assert(!mInputMap.ContainsKey(item.Key), $"중복된 요소 : {item.Key}");
                mInputMap.Add(item.Key, item.Ability);
            }
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
            SetAbilityKeys(EditorInputMap);
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
                    KeyInputEvent.Invoke(input_Ability.Key, input_Ability.Value);
                }
            }
        }

    }
}