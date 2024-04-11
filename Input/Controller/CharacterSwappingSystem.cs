using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static RPG.Input.ActionKey;

namespace RPG.Input.Controller
{
    public class CharacterSwappingSystem : InputInteraction
    {
        Controller mController;
        public override bool IsAble => mController.IsActive &&
                              delay + 0.5f <= Time.time;
        float delay;
        int mIndex;

        public CharacterSwappingSystem(Controller controller) : base()
        {
            mController = controller;
        }

        void Swapping(float input)
        {
            if (input.Equals(0f))
            {
                return;
            }

            delay = Time.time;

            mController.AllControlledTargets[mIndex++].OnReleaseControll();
            // 루프 타일링
            mIndex = mIndex == mController.AllControlledTargets.Count ? 0 :
                                                      mIndex;
            //
            mController.BindObject(mController.AllControlledTargets[mIndex]);
        }

        protected override void MappingInputEvent()
        {
            InputEventMap = new Dictionary<string, UnityAction<float>>()
            {
                { TAB, Swapping }
            };
        }
    }
}

