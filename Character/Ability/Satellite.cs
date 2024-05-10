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
            Debug.Assert(mSatellite, "���� �������� ã�� �� ����.");
            Debug.Assert(mMovement, "�˵� ��ȸ �����Ʈ�� ã�� �� ����.");

            var attacker = collision.Attacker;
            var owner = attacker.transform;

            var satellite = Instantiate(mSatellite, owner) as Character;
            satellite.Movement.PlayMovement(mMovement);
        }
    }
    
}