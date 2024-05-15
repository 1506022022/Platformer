using PlatformGame.Character.Combat;
using PlatformGame.Input;
using PlatformGame.Pipeline;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace PlatformGame.Character.Controller
{
    [Serializable]
    public class ActionDataKeyPair
    {
        public string Key;
        public ActionData ActionData;
    }

    public class PlayerCharacterController : MonoBehaviour
    {
        [FormerlySerializedAs("InputMap")]
        [HideInInspector] public List<ActionDataKeyPair> InputMap;
        [FormerlySerializedAs("BeforeTarget")]
        [HideInInspector] public Character BeforeTarget;
        public UnityEvent<ActionDataKeyPair> InputEvents;
        public bool IsActive
        {
            get; private set;
        }
        public Character ControlledCharacter;
        Dictionary<string, ActionData> mInputMap;
        Pipeline<ActionDataKeyPair> mInputPipeline;

        public void SetActive(bool able)
        {
            IsActive = able;
            FocusOn(able);
        }

        void SetInputAction(List<ActionDataKeyPair> actionKeys)
        {
            mInputMap = new Dictionary<string, ActionData>();
            foreach (var item in actionKeys)
            {
                Debug.Assert(!mInputMap.ContainsKey(item.Key), $"duplicate elements : {item.Key}");
                mInputMap.Add(item.Key, item.ActionData);
            }
        }

        void EnterCommand(ActionDataKeyPair input)
        {
            var actionID = input.ActionData.ID;
            ControlledCharacter.DoAction(actionID);
        }

        void FocusOn(bool on)
        {
            if (!ControlledCharacter.UI)
            {
                return;
            }

            ControlledCharacter.UI.SetActive(on);
        }

        void Awake()
        {
            SetInputAction(InputMap);

            mInputPipeline = Pipelines.Instance.PlayerCharacterControllerPipeline;
            mInputPipeline.InsertPipe(EnterCommand);
            mInputPipeline.InsertPipe((x) => InputEvents.Invoke(x));
        }

        void Update()
        {
            if (!IsActive)
            {
                return;
            }

            var map = ActionKey.GetKeyDownMap();
            foreach (var input_Action in mInputMap)
            {
                var input = map[input_Action.Key];
                if (!input)
                {
                    continue;
                }

                mInputPipeline.Invoke(new ActionDataKeyPair()
                    { Key = input_Action.Key, ActionData = input_Action.Value });
            }
        }
        
    }
}