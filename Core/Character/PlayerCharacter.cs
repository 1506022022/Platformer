using PlatformGame.Character.Combat;
using UnityEngine;

namespace PlatformGame.Character
{
    public class PlayerCharacter : Character
    {
        [Header("[PlayerCharacter]")]
        [SerializeField]
        AbilityDataList mHasAbilities;

        public AbilityDataList HasAbilities => mHasAbilities;
        [SerializeField] GameObject mUI;
        public GameObject UI => mUI;
        public bool IsAction => mAbility.IsAction;
        Ability mAbility;

        public void DoAction(uint combatID)
        {
            if (mAbility.IsAction)
            {
                return;
            }

            mHasAbilities.Library.TryGetValue(combatID, out var abilityData);
            Debug.Assert(abilityData.ID != 0);

            if (!StateCheck.Equals(State, abilityData.AllowedState))
            {
                return;
            }
            State = abilityData.BeState;

            mAbility.Action(abilityData);

            if (!abilityData.Movement)
            {
                return;
            }
            Movement.PlayMovement(abilityData.Movement);
        }

        protected override void Awake()
        {
            base.Awake();
            mAbility = new Ability(this);
        }
    }
}