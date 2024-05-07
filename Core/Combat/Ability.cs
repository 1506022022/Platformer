using PlatformGame.Character.Collision;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    public class Ability
    {
        HitBox mHitBox;
        float mLastActionTime;
        AbilityData mLastAbilitytData;

        public Ability(HitBox hitBox)
        {
            mHitBox = hitBox;
        }
        public Ability()
        {

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
            HitBox hitBox = null;
            if (hitBoxData.UseSelfHitBox)
            {
                hitBox = mHitBox;
            }

            if (hitBox == null ||
                hitBoxData.HitBoxNames.Count == 0)
            {
                return;
            }

            Debug.Assert(hitBox != null);
            hitBox.Flags.SetFlag(abilityData.HitBoxData.Flags);
            hitBox.SetAttackCallback(hitBoxData.HitBoxNames, abilityData.HitedEvent.Action);
        }
    }
}