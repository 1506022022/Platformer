﻿using PlatformGame.Character.Combat;
using UnityEngine;

namespace PlatformGame.Character
{
    public class PlayerCharacter : Character
    {
        [Header("[PlayerCharacter]")]
        [SerializeField] AbilityDataList mHasAbilities;
        public AbilityDataList HasAbilities => mHasAbilities;
        [SerializeField] GameObject mUI;
        public GameObject UI => mUI;
        Ability mAbility;

        public void CombatTo(uint combatID)
        {
            if (mAbility.IsAction)
            {
                return;
            }

            AbilityData abilityData;
            mHasAbilities.Library.TryGetValue(combatID, out abilityData);
            Debug.Assert(abilityData.ID != 0);

            if (!StateCheck.Equals(State, abilityData.AllowedState))
            {
                return;
            }

            State = abilityData.BeState;
            if(!State.Equals(CharacterState.Attack) && !State.Equals(CharacterState.Jumping))
            {
                ReturnBasicState();
            }
            mAbility.Action(abilityData);

            if (abilityData.Movement == null)
            {
                return;
            }
            Movement.PlayMovement(abilityData.Movement);
        }

        protected override void Update()
        {
            if (mAbility.IsAction)
            {
                State = CharacterState.AttackDelay;
                return;
            }
            base.Update();
            Debug.Log(State);
        }

        protected override void Awake()
        {
            base.Awake();
            mAbility = new Ability(this);
        }

    }
}