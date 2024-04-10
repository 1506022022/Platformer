using RPG.Input.Controller;
using UnityEngine;

namespace RPG.Character
{
    public class Character : MonoBehaviour, IControllableObject
    {
        Combat mCombat;
        Status mStatus;
        CharacterCamera mCharCamera;
        CharacterAnimation mCharAnim;

        [Header("[Component]")]
        [SerializeField] Animator mAnimator;
        [SerializeField] Rigidbody mRigid;
        [SerializeField] Camera mCam;
        [SerializeField] GameObject mUI;

        [Header("[Debug]")]
        [SerializeField] State mState = State.Idle;

        // TODO : ���߿� �̵� ���� ����ü(�̵��ӵ�, �̵����ɻ���, ����.. ��) ���� ��ü
        [SerializeField] float mMoveSpeed = 1;
        public float GetMoveSpeed()
        {
            return mMoveSpeed;
        }
        // TODOEND

        public Rigidbody GetRigidbody()
        {
            Debug.Assert(mRigid);
            return mRigid;
        }
        public Camera GetCamera()
        {
            Debug.Assert(mCam);
            return mCam;
        }
        public bool IsControllable()
        {
            return mCharAnim.IsGround();
        }
        public void SetControlledTarget()
        {
            Debug.Assert(mUI);
            mUI.SetActive(true);
            GetCamera().gameObject.SetActive(true);
        }
        public void ReleaseControlledTarget()
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
