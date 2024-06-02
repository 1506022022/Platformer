using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/Pipeline/AbilityLog")]
    public class AbilityLog : ScriptableObject
    {
        static Dictionary<int, UnityEvent<AbilityCollision>> mHitCallback = new();
        public static void AddHitCallback(int instanceID, UnityAction<AbilityCollision> call)
        {
            if (!mHitCallback.ContainsKey(instanceID))
            {
                mHitCallback.Add(instanceID, new UnityEvent<AbilityCollision>());
            }
            mHitCallback[instanceID].AddListener(call);
        }
        public static void RemoveHitCallback(int instanceID, UnityAction<AbilityCollision> call)
        {
            if (!mHitCallback.ContainsKey(instanceID))
            {
                return;
            }
            mHitCallback[instanceID].RemoveListener(call);
        }

        public void OnHit(AbilityCollision collision)
        {
            var victim = collision.Victim.GetInstanceID();
            if(mHitCallback.ContainsKey(victim))
            {
                mHitCallback[victim].Invoke(collision);
            }

            var attacker = collision.Caster.GetInstanceID();
            if (mHitCallback.ContainsKey(attacker))
            {
                mHitCallback[attacker].Invoke(collision);
            }
        }
    }

}
