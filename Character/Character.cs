using UnityEngine;

namespace RPG.Character
{
    public class Character : MonoBehaviour
    {
        [Header("[Debug]")]
        [SerializeField] protected State mState = State.Idle;

        Combat mCombat;
        Status mStatus;
        CharacterCamera mCharCamera;
        CharacterAnimation mCharAnim;

        [Header("[Component]")]
        [SerializeField] Animator mAnimator;
        [SerializeField] Rigidbody mRigid;
        [SerializeField] Camera mCam;
        [SerializeField] GameObject mUI;


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
        void Awake()
        {
            mCombat = new Combat();
            mStatus = new Status();
            mCharAnim = new CharacterAnimation(mAnimator, mRigid);
            mCharCamera = new CharacterCamera();
        }
        void Update()
        {
            mState = mCharAnim.UpdateAndGetState();
        }

    }
}
