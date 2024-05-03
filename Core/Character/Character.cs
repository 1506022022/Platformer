using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Character
{
    public abstract class Character<T> : MonoBehaviour where T : Character<T>
    {
        static List<T> instances = new List<T>();
        public static List<T> Instances
        {
            get
            {
                Debug.Assert(instances.Count > 0, $"Not found {typeof(T).Name}");
                return instances.ToList();
            }
            private set
            {
                instances = value;
            }
        }
        public State State { get; protected set; } = State.Idle;
        Status.Status mStatus;
        CharacterCamera mCharCamera;
        List<ITransitionAnimation> mTransitionAnimationList = new List<ITransitionAnimation>();

        // TODO : 나중에 이동 관련 구조체(이동속도, 이동가능상태, 버프.. 등) 만들어서 대체
        public float MoveSpeed { get; } = 1.0f;
        // TODOEND

        protected virtual void Update()
        {
            foreach (var transitionAnim in mTransitionAnimationList)
            {
                if (transitionAnim.IsTransitionAbleState(State))
                {
                    State = transitionAnim.UpdateAndGetState();
                }
            }
        }
        protected virtual void Awake()
        {
            instances.Add((T)this);
            // TODO : 스테이터스, 카메라 연출 기능 구현
            mStatus = new Status.Status();
            mCharCamera = new CharacterCamera();
            // TODOEND
            var transitionAnims = GetComponents<ITransitionAnimation>().ToList();
            mTransitionAnimationList.AddRange(transitionAnims);
        }
        void OnDestroy()
        {
            instances.Remove((T)this);
        }
    }
}
