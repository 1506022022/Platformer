using UnityEngine;

namespace Platformer.Core
{
    public class Combat
    {
        HitBox mHitBox;
        float mLastActionTime;
        CombatData mLastCombatData;

        public Combat(HitBox hitBox)
        {
            mHitBox = hitBox;
        }
        public bool IsAction
        {
            get
            {
                return mLastCombatData.ID == 0 ? false : Time.time < mLastActionTime + mLastCombatData.ActionTime;
            }
        }

        public void Action(CombatData combatData)
        {
            var hitBoxData = combatData.HitBoxData;
            HitBox hitBox = null;
            if (hitBoxData.UseSelfHitBox)
            {
                hitBox = mHitBox;
            }
            Debug.Assert(hitBox != null);
            hitBox.Flags.SetFlag(combatData.HitBoxData.Flags);
            hitBox.SetAttackCallback(hitBoxData.HitBoxNames, combatData.HitedEvent.Action);
            mLastActionTime = Time.time;
            mLastCombatData = combatData;
        }
    }
}