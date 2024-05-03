using Platformer.Core;
using RPG.Input.Controller;
using UnityEngine;

namespace RPG.Character
{
    public class PlayerCharacter : Character<PlayerCharacter>
    {
        Combat mCombat;
        XZMovement mMovement;
        JumpMovement mJumpMovement;
        [Header("[Component]")]
        [SerializeField] Camera mCam;
        [SerializeField] GameObject mUI;
        [SerializeField] MovementAnimation mCharacterAnim;
        [SerializeField] Rigidbody mRigid;
        [SerializeField] CombatDataList mCombatDatas;
        [SerializeField] HitBox mAttackHitBox;

        public void MovePos(Vector3 pos)
        {
            SetAbilityState(State.Running);
        }

        public void MoveDir(Vector3 dir)
        {
            if (State == State.Attack)
            {
                return;
            }
            mMovement.Move(dir);
        }

        public void Jump()
        {
            if (State == State.Idle ||
                State == State.Walk ||
                State == State.Running)
            {
                mJumpMovement.Jump();
                SetAbilityState(State.Jumping);
            }
            
        }

        public void Combat(uint combatID)
        {
            CombatData combatData;
            mCombatDatas.Library.TryGetValue(combatID, out combatData);
            Debug.Assert(combatData.ID != 0);
            if (!mCombat.IsAction)
            {
                SetAbilityState(State.Attack);
                mCombat.Action(combatData);
                mCharacterAnim.PlayAnimation(combatData.AnimationName);
            }
        }

        protected override void Update()
        {
            if (!mCombat.IsAction)
            {
                ReturnBasicState();
                mCharacterAnim.UpdateAnimation(State);
            }
        }
        void ReturnBasicState()
        {
            Debug.Assert(!mCombat.IsAction);
            if ((Platformer.RigidbodyUtil.IsGrounded(mRigid) && mJumpMovement.IsDelay) ||
                !Platformer.RigidbodyUtil.IsGrounded(mRigid))
            {
                State = (mRigid.velocity.y >= 0) ? State.Jumping :
                                                    State.Falling;
            }
            else
            {
                State = (Mathf.Abs(mRigid.velocity.magnitude) < 0.01f) ? State.Idle :
        (mRigid.velocity.magnitude < 2f) ? State.Walk :
                                           State.Running;
            }
        }
        protected override void Awake()
        {
            base.Awake();
            mCombat = new Combat(mAttackHitBox);
            mMovement = new XZMovement(mRigid, mCam);
            mJumpMovement = new JumpMovement(mRigid);
        }
        void SetAbilityState(State state)
        {
            State = state;
        }

        // TODO : 제거
        public Camera GetCamera()
        {
            Debug.Assert(mCam);
            return mCam;
        }
        // TODOEND

        // TODO : 캐릭터 컨트롤러로 이동
        public void FocusOn()
        {
            Debug.Assert(mUI);
            mUI.SetActive(true);
            GetCamera().gameObject.SetActive(true);
        }
        public void FocusOff()
        {
            Debug.Assert(mUI);
            mUI.SetActive(false);
            GetCamera().gameObject.SetActive(false);
        }
        // TODOEND
    }
}
