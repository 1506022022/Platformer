using PlatformGame.Character.Collision;
using PlatformGame.Character.Combat;
using PlatformGame.Character.Movement;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Character
{
    public class Character : MonoBehaviour
    {
        public bool IsAction => mAgent.InAction;
        CharacterState mState;
        public CharacterState State
        {
            get => mState;
            set
            {
                if (mState != value)
                {
                    OnChangedState.Invoke(value);
                }
                mState = value;
            }
        }

        [Header("References")]
        [SerializeField] GameObject mUI;
        [SerializeField] HitBoxGroup mHitBox;
        public GameObject UI => mUI;
        [SerializeField] Rigidbody mRigid;
        public Rigidbody Rigid => mRigid;
        [SerializeField] MovementComponent mMovement;
        public MovementComponent Movement => mMovement;
        [SerializeField] Transform mModel;
        public Transform Model => mModel;


        [Header("Controls")]
        [SerializeField] AbilityAgent mAgent;
        [SerializeField] ActionDataList mHasAbilities;
        public ActionDataList HasAbilities => mHasAbilities;
        [SerializeField] UnityEvent<CharacterState> mOnChangedState;
        public UnityEvent<CharacterState> OnChangedState => mOnChangedState;


        public void DoAction(uint actionID)
        {
            mHasAbilities.Library.TryGetValue(actionID, out var action);
            Debug.Assert(action, $"{actionID}가 {gameObject.name}의 능력으로 등록되지 않음.");

            if (!StateCheck.Equals(State, action.AllowedState))
            {
                return;
            }
            State = action.BeState;
            mAgent.UseAbility(action);

            if (!action.Movement)
            {
                return;
            }
            Movement.PlayMovement(action.Movement);
        }

        public void SetAttackDelayState()
        {
            State = CharacterState.AttackDelay;
        }

        void Awake()
        {
            Debug.Assert(mHitBox, $"HitBox reference not found : {gameObject.name}");
            Debug.Assert(Rigid, $"Rigidbody reference not found : {gameObject.name}");
            Debug.Assert(mMovement, $"Movement reference not found : {gameObject.name}");
            Debug.Assert(mModel, $"Model reference not found : {gameObject.name}");
            mAgent = new AbilityAgent(mHitBox);
            OnChangedState.Invoke(State);
        }

    }
}