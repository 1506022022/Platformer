using RPG.Input;
using RPG.Input.Controller;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static RPG.Input.ActionKey;

namespace RPG
{
    [RequireComponent(typeof(Controller))]
    public class CharacterSwappingSystem : MonoBehaviour, IInputInteraction
    {
        float delay;
        int mIndex;
        Controller mController;
        List<Character.Character> mCharacters = new List<Character.Character>();

        public Dictionary<string, UnityAction<float>> InputEventMap { get; private set; }

        public bool IsAble()
        {
            return mController.IsActive &&
                   mCharacters.Count > 0 &&
                   delay + 0.5f <= Time.time;
        }
        public void BindCharacters(List<Character.Character> characters)
        {
            mCharacters = characters;
            mIndex = 0;
            Debug.Assert(mCharacters.Count > 0);
        }
        public void ReleaseCharacters()
        {

            var currentCharacter = mCharacters[mIndex];
            var target = IInputInteractionFactory.ConvertFromCharacter(currentCharacter);
            mController.RemoveInputInteractionTargets(target);
            mCharacters.Clear();
        }
        void Awake()
        {
            mController = GetComponent<Controller>();
            MappingInputEvent();
        }
        void MappingInputEvent()
        {
            InputEventMap = new Dictionary<string, UnityAction<float>>()
            {
                { TAB, Swapping }
            };
        }
        void Swapping(float input)
        {
            if (input.Equals(0f))
            {
                return;
            }

            delay = Time.time;
            var beforeTarget = mCharacters[mIndex++];
            beforeTarget.FocusOff();
            var inputInteractions = beforeTarget.GetComponents<IInputInteraction>().ToList();
            mController.RemoveInputInteractionTargets(inputInteractions);

            // 루프 타일링
            mIndex = (mIndex == mCharacters.Count) ? 0
                                                   : mIndex;
            //
            var currentTarget = mCharacters[mIndex];
            currentTarget.FocusOn();
            inputInteractions = currentTarget.GetComponents<IInputInteraction>().ToList();
            mController.AddInputInteractionTargets(inputInteractions);
        }
    }
}

