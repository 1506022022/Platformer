using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Character
{
    public class Character : MonoBehaviour
    {
        static List<Character> instances = new List<Character>();
        public static List<Character> Instances
        {
            get
            {
                Debug.Assert(instances.Count > 0,$"Not found Character : {instances.Count}");
                return instances.ToList();
            }
            private set
            {
                instances = value;
            }
        }
        public Combat Combat
        {
            get;
            private set;
        }

        [Header("[Debug]")]
        [SerializeField] protected State mState = State.Idle;

        Status mStatus;
        CharacterCamera mCharCamera;
        List<ITransitionAnimation> mTransitionAnimationList = new List<ITransitionAnimation>();

        [Header("[Component]")]
        [SerializeField] Camera mCam;
        [SerializeField] GameObject mUI;
        [SerializeField] Rigidbody mRigid;
        [SerializeField] Animator mAnimator;


        // TODO : 나중에 이동 관련 구조체(이동속도, 이동가능상태, 버프.. 등) 만들어서 대체
        public float MoveSpeed { get; } = 1.0f;
        // TODOEND
        public Camera GetCamera()
        {
            Debug.Assert(mCam);
            return mCam;
        }
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
        void CreateMovementAnimAndUse()
        {
            var MovementAnim = new MovementAnimation();
            mTransitionAnimationList.Add(MovementAnim);
        }
        void CreateCombatAndUse()
        {
            Combat = new Combat();
            mTransitionAnimationList.Add(Combat);
        }
        void BindTransitionAnimations()
        {
            foreach (var transitionAnim in mTransitionAnimationList)
            {
                transitionAnim.SetAnimationTarget(mAnimator, mRigid);
            }
        }
        void Update()
        {
            // 애니메이션, 상태 업데이트
            foreach (var transitionAnim in mTransitionAnimationList)
            {
                if (transitionAnim.IsTransitionAbleState(mState))
                {
                    mState = transitionAnim.UpdateAndGetState();
                }
            }
            //
        }
        void Awake()
        {
            CreateCombatAndUse();
            CreateMovementAnimAndUse();
            BindTransitionAnimations();

            // TODO : 스테이터스, 카메라 연출 기능 구현
            mStatus = new Status();
            mCharCamera = new CharacterCamera();
            // TODOEND

            instances.Add(this);
        }
        void OnDestroy()
        {
            instances.Remove(this);
        }
    }
}
