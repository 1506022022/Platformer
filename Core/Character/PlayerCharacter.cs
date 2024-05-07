using PlatformGame.Character.Collision;
using PlatformGame.Character.Combat;
using UnityEngine;
using static PlatformGame.Character.Status.MovementInfo;

namespace PlatformGame.Character
{
    public class PlayerCharacter : Character
    {
        Ability mAbility;
        [Header("[PlayerCharacter]")]
        [SerializeField] AbilityDataList mHasAbilities;
        public AbilityDataList HasAbilities => mHasAbilities;
        [SerializeField] GameObject mUI;
        public GameObject UI => mUI;
        [SerializeField] HitBox mAttackHitBox;

        public void CombatTo(uint combatID)
        {
            AbilityData combatData;
            mHasAbilities.Library.TryGetValue(combatID, out combatData);
            Debug.Assert(combatData.ID != 0);

            if (!StateCheck.Equals(State, combatData.AllowedState))
            {
                return;
            }

            if (mAbility.IsAction)
            {
                return;
            }

            State = combatData.BeState;
            mAbility.Action(combatData);

            if (combatData.MovementAction == null)
            {
                return;
            }
            mMovement.PlayMovement(combatData.MovementAction);
        }

        protected override void Update()
        {
            if (mAbility.IsAction) return;
            base.Update();
            LimitMoveSpeed();
        }

        void Awake()
        {
            mAbility = mAttackHitBox == null ?
            new Ability() :
            new Ability(mAttackHitBox);
        }

        // TODO : 분리
        void LimitMoveSpeed()
        {
            var mVelocity = mRigid.velocity;
            mVelocity.x = Mathf.Clamp(mVelocity.x, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mVelocity.z = Mathf.Clamp(mVelocity.z, -MAX_MOVE_VELOCITY, MAX_MOVE_VELOCITY);
            mRigid.velocity = mVelocity;
        }
        // TODOEND
    }
}