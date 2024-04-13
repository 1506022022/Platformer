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
                Debug.Assert(instances.Count > 0, $"Not found Character : {instances.Count}");
                return instances.ToList();
            }
            private set
            {
                instances = value;
            }
        }
        [SerializeField] public State State { get; private set; } = State.Idle;

        Status.Status mStatus;
        CharacterCamera mCharCamera;
        List<ITransitionAnimation> mTransitionAnimationList = new List<ITransitionAnimation>();

        [Header("[Component]")]
        [SerializeField] Camera mCam;
        [SerializeField] GameObject mUI;

        // TODO : ���߿� �̵� ���� ����ü(�̵��ӵ�, �̵����ɻ���, ����.. ��) ���� ��ü
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
        void Update()
        {
            foreach (var transitionAnim in mTransitionAnimationList)
            {
                if (transitionAnim.IsTransitionAbleState(State))
                {
                    State = transitionAnim.UpdateAndGetState();
                }
            }
        }
        void Awake()
        {
            instances.Add(this);
            // TODO : �������ͽ�, ī�޶� ���� ��� ����
            mStatus = new Status.Status();
            mCharCamera = new CharacterCamera();
            // TODOEND
            var transitionAnims = GetComponents<ITransitionAnimation>().ToList();
            mTransitionAnimationList.AddRange(transitionAnims);
        }
        void OnDestroy()
        {
            instances.Remove(this);
        }
    }
}
