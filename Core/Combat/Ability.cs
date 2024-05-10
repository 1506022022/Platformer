using PlatformGame.Character.Collision;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    public struct CoolTime
    {
        public float EndTime;
        public uint ID;
    }

    public class Ability
    {
        readonly Character mActor;
        float mLastActionTime;
        AbilityData mLastAbilityData;

        public Ability(Character actor)
        {
            mActor = actor;
        }

        public bool IsAction
        {
            get { return mLastAbilityData.ID != 0 && Time.time < mLastActionTime + mLastAbilityData.ActionDelay; }
        }

        public void Action(AbilityData abilityData)
        {
            mLastActionTime = Time.time;
            mLastAbilityData = abilityData;
            var hitBoxData = abilityData.HitBoxData;

            var filter = hitBoxData.Filter;
            if (filter.Count == 0)
            {
                return;
            }

            HitBox hitBox = null;
            if (hitBoxData.UseSelfHitBox)
            {
                hitBox = mActor.HitBox;
            }

            if (!hitBox)
            {
                return;
            }

            hitBox.SetAttackCollidersFlags(filter, abilityData.HitBoxData.Flags);

            if (!abilityData.Ability)
            {
                return;
            }

            hitBox.SetAttackCallback(filter, abilityData.Ability.Action);
        }
    }
}