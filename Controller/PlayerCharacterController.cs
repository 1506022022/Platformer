using RPG.Character;
using RPG.Input;
using RPG.Input.Controller;
using System.Linq;

namespace RPG
{
    public class PlayerCharacterController : Controller
    {
        Character.Character mPlayerCharacter;
        public void SetControllCharacter(Character.Character character)
        {
            mPlayerCharacter = character;
            mPlayerCharacter.FocusOn();

            var characterAbilitys = character.GetComponents<Ability>()
                                             .ToList<IInputInteraction>();

            var xzMovement = character.GetComponent<XZMovement>();
            if(xzMovement != null)
            {
                xzMovement.PublicCondition += () =>
                    character.State == State.Idle ||
                    character.State == State.Running;
            }

            var jumpMovement = character.GetComponent<JumpMovement>();
            if(jumpMovement != null)
            {
                jumpMovement.PublicCondition += () =>
                    character.State == State.Idle ||
                    character.State == State.Running;
            }

            ReleaseControll();
            AddInputInteractions(characterAbilitys);
        }
        public void ReleaseControll()
        {
            ClearInputInteractionTargets();
        }
    }
}


