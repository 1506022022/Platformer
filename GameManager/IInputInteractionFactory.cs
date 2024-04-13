using RPG.Character;
using RPG.Input;
using RPG.Input.Controller;
using System.Collections.Generic;
using System.Linq;

namespace RPG
{
    public static class IInputInteractionFactory
    {
        public static List<IInputInteraction> ConvertFromCharacter(Character.Character character)
        {
            var characterInputInteractions = character.GetComponents<IInputInteraction>().ToList();

            var xzMovement = character.GetComponent<XZMovement>();
            xzMovement.ConditionOfMoveable = () =>
                character.State == State.Idle ||
                character.State == State.Running;

            var jumpMovement = character.GetComponent<JumpMovement>();
            jumpMovement.ConditionOfMoveable = () =>
                character.State == State.Idle ||
                character.State == State.Running;

            return characterInputInteractions;
        }
    }
}


