using PlatformGame.Character.Combat;
using PlatformGame.Tool;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Character.Controller
{
    [RequireComponent(typeof(Character))]
    public class DefaultStateController : MonoBehaviour
    {
        PlayerCharacter mCharacter;
        public AbilityData Fall;
        public AbilityData Land;
        public AbilityData Idle;
        public AbilityData Walk;
        public AbilityData Run;

        void Awake()
        {
            Debug.Assert(Fall.ID != 0);
            Debug.Assert(Land.ID != 0);
            Debug.Assert(Idle.ID != 0);
            Debug.Assert(Walk.ID != 0);
            Debug.Assert(Run.ID != 0);

            mCharacter = GetComponent<PlayerCharacter>();
        }

        void Update()
        {
            if (mCharacter.IsAction)
            {
                return;
            }

            var velY = Math.Round(mCharacter.Rigid.velocity.y, 1);
            if (!RigidbodyUtil.IsGrounded(mCharacter.Rigid) && velY != 0)
            {
                mCharacter.DoAction(Fall.ID);
                return;
            }

            if (Mathf.Abs(mCharacter.Rigid.velocity.magnitude) < 0.01f)
            {
                mCharacter.DoAction(Idle.ID);
                return;
            }

            if (mCharacter.Rigid.velocity.magnitude < 2f)
            {
                mCharacter.DoAction(Walk.ID);
                return;
            }

            else
            {
                mCharacter.DoAction(Run.ID);
            }
        }

    }
}
