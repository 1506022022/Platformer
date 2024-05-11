using PlatformGame.Character.Collision;
using PlatformGame.Character.Movement;
using PlatformGame.Tool;
using System;
using UnityEngine;

namespace PlatformGame.Character
{
    [RequireComponent(typeof(HitBox))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(MovementComponent))]
    public class Character : MonoBehaviour
    {
        [Header("[Character]")]
#if UNITY_EDITOR
        [SerializeField]
#endif
        CharacterState mState;

        public CharacterState State
        {
            get => mState;
            protected set => mState = value;
        }

        public HitBox HitBox { get; private set; }
        public Rigidbody Rigid { get; private set; }
        public MovementComponent Movement { get; private set; }

        protected virtual void Awake()
        {
            HitBox = GetComponent<HitBox>();
            Rigid = GetComponent<Rigidbody>();
            Movement = GetComponent<MovementComponent>();
        }

    }
}