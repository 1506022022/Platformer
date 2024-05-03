using RPG.Character;
using RPG.Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static RPG.Input.ActionKey;

public class CharacterSwap// : IInputInteraction
{
    //float mDelay;
    //int mCharacterIndex;
    //PlayerCharacterController mController;
    //public Dictionary<string, UnityAction<float>> InputEventMap { get; private set; }

    //public CharacterSwap(PlayerCharacterController controller)
    //{
    //    Debug.Assert(controller != null);
    //    mController = controller;
    //    MappingInputEvent();
    //}
    //void MappingInputEvent()
    //{
    //    InputEventMap = new Dictionary<string, UnityAction<float>>()
    //    {
    //        { SWAP, (f) => {if(!f.Equals(0)) SwapCharacter();  } }
    //    };
    //}
    //public void SwapCharacter()
    //{
    //    Debug.Assert(PlayerCharacter.Instances.Count > 0);

    //    var character = PlayerCharacter.Instances[mCharacterIndex];
    //    // TODO : 위치 고려
    //    character.FocusOff();
    //    // TODOEND

    //    mCharacterIndex = PlayerCharacter.Instances.Count <= (mCharacterIndex + 1) ? 0 : mCharacterIndex + 1;
    //    character = PlayerCharacter.Instances[mCharacterIndex];

    //    mController.SetControllCharacter(character);
    //    character.FocusOn();

    //    mDelay = Time.time + 0.5f;
    //}

    //public bool IsAbleNow()
    //{
    //    return mDelay < Time.time;
    //}
}
