using PlatformGame.Character.Collision;
using PlatformGame.Pipeline;
using System;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    public abstract class Ability : ScriptableObject
    {
        Pipeline<AbilityCollision> mPipeline;

        public void DoActivation(CollisionData collision)
        {
            CreatePipeline();
            mPipeline.Invoke(new AbilityCollision(collision, this));
        }

        public abstract void UseAbility(CollisionData collision);

        // TODO : 검토
        void CreatePipeline()
        {
            mPipeline = Pipelines.Instance.AbilityPipeline;
            mPipeline.InsertPipe((collision) => UseAbility(collision.Data));
        }
        //
    }
}