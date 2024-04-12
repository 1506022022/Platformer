using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static RPG.Input.ActionKey;

namespace RPG.Input.Controller
{
    public class CharacterSwappingSystem : InputInteraction
    {
        float delay;
        int mIndex;
        Controller mController;
        List<ControllableCharacter> mCharacters = new List<ControllableCharacter>();

        public CharacterSwappingSystem(Controller controller) : base()
        {
            mController = controller;
        }
        public override bool IsAble()
        {
            return mController.IsActive &&
                   delay + 0.5f <= Time.time;
        }
        public void BindCharacters(List<ControllableCharacter> characters)
        {
            mCharacters = characters;
            mIndex = 0;
            Debug.Assert(mCharacters.Count > 0);
        }
        protected override void MappingInputEvent()
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
            beforeTarget.OnReleaseControll();
            mController.RemoveInputInteractionTarget(beforeTarget);

            // 루프 타일링
            mIndex = (mIndex == mCharacters.Count) ? 0
                                                   : mIndex;
            //
            var currentTarget = mCharacters[mIndex];
            currentTarget.OnBindControll();
            mController.AddInputInteractionTarget(currentTarget);
        }
    }
}

