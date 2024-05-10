using PlatformGame.Character.Collision;
using PlatformGame.Character.Movement;
using UnityEngine;

namespace PlatformGame.Character.Combat
{
    [CreateAssetMenu(menuName = "Custom/AbilityAction/Satellite")]
    public class Satellite : AbilityAction
    {
        [SerializeField] Character mSatellite;
        [SerializeField] SatelliteMovement mMovement;

        public override void Action(CollisionData collision)
        {
            Debug.Assert(mSatellite, "위성 프리펩을 찾을 수 없음.");
            Debug.Assert(mMovement, "궤도 순회 무브먼트를 찾을 수 없음.");

            var attacker = collision.Attacker;
            var owner = attacker.transform;

            var satellite = Instantiate(mSatellite, owner) as Character;
            satellite.Movement.PlayMovement(mMovement);
        }

    }
}
