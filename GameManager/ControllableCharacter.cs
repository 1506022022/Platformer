using RPG.Character;
using RPG.Input;
using RPG.Input.Controller;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static RPG.Input.ActionKey;

namespace RPG
{
    public class ControllableCharacter : InputInteraction
    {
        Rigidbody mObjectRigid;
        XZMovement mXZMovement;
        JumpMovement mJumpMovement;
        Character.Character mCharacter;
        public ControllableCharacter(Character.Character character) : base()
        {
            mCharacter = character;
            mObjectRigid = character.GetComponent<Rigidbody>();
            Debug.Assert(mObjectRigid);
            var cam = character.GetCamera();
            var moveSpeed = character.MoveSpeed;

            mXZMovement = new XZMovement(mObjectRigid, cam, moveSpeed);
            mJumpMovement = new JumpMovement(mObjectRigid);
        }

        protected override void MappingInputEvent()
        {
            InputEventMap = new Dictionary<string, UnityAction<float>>()
            {
                { VERTICAL,     (f) => mXZMovement.UpdateVerticalMovement(f) },
                { HORIZONTAL,   (f) =>mXZMovement.UpdateHorizontalMovement(f) },
                { JUMP,         (f) => mJumpMovement.Jump(f) }
            };
        }
        public override bool IsAble()
        {
            return RigidbodyUtil.IsGround(mObjectRigid);
        }
        public void OnBindControll()
        {
            mCharacter.FocusOn();
        }
        public void OnReleaseControll()
        {
            mCharacter.FocusOff();
        }
    }
}
