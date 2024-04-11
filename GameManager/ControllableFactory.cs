using RPG.Input.Controller;
using System;
using UnityEngine;

namespace RPG
{
    public static class ControllableFactory
    {
        public static IControllableObject CreateFromCharacter(Character.Character character)
        {
            var moveSpeed = character.MoveSpeed;
            var camera = character.GetCamera();
            var rigid = character.GetComponent<Rigidbody>();
            Func<bool> isControllable = () => RigidbodyUtil.IsGround(rigid);

            ControllableObject obj = new ControllableObject(moveSpeed,
                                                            camera,
                                                            rigid,
                                                            isControllable,
                                                            character.FocusOn,
                                                            character.FocusOff);

            IControllableObject controllableObject = obj;
            return controllableObject;
        }
    }
}
