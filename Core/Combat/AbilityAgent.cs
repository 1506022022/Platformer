using PlatformGame.Character.Collision;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Character.Combat
{
    public class AbilityAgent
    {
        readonly HitBoxGroup mHitBox;
        float mLastActionTime;
        ActionData mLastUsedAction;
        [SerializeField] UnityEvent<Character, uint> mAttackEvent;

        public AbilityAgent(HitBoxGroup hitBox)
        {
            mHitBox = hitBox;
        }

        public bool InAction
        {
            get { return mLastUsedAction != null && Time.time < mLastActionTime + mLastUsedAction.ActionDelay; }
        }

        public void UseAbility(ActionData actionData)
        {
            mLastActionTime = Time.time;
            mLastUsedAction = actionData;
            var hitBoxData = actionData.HitBoxData;

            if (!hitBoxData.UseHitBox)
            {
                return;
            }
            var filter = hitBoxData.Filter;
            mHitBox.SetAttackCollidersFlags(filter, actionData.HitBoxData.Flags);

            if (!actionData.Ability)
            {
                return;
            }
            mHitBox.SetAttackEvent(filter, actionData.Ability.DoActivation, actionData.ID);
        }
    }
}