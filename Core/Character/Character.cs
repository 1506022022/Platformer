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
            // TODO : 스테이터스, 카메라 연출 기능 구현
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
