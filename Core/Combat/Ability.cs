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
        Character mActor;
        float mLastActionTime;
        AbilityData mLastAbilitytData;

        public Ability(Character actor)
        {
            mActor = actor;
        }

        public bool IsAction
        {
            get
            {
                return mLastAbilitytData.ID == 0 ?
                    false :
                    Time.time < mLastActionTime + mLastAbilitytData.ActionDelay;
            }
        }

        public void Action(AbilityData abilityData)
        {
            mLastActionTime = Time.time;
            mLastAbilitytData = abilityData;
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

            if (hitBox == null)
            {
                return;
            }
            hitBox.SetAttackCollidersFlags(filter, abilityData.HitBoxData.Flags);

            if(abilityData.Ability == null)
            {
                return;
            }
            hitBox.SetAttackCallback(filter, abilityData.Ability.Action);
        }

    }
}