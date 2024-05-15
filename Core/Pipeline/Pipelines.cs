using PlatformGame.Character.Collision;
using PlatformGame.Character.Combat;
using PlatformGame.Character.Controller;
using PlatformGame.Pipeline;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Pipelines")]
public class Pipelines : ScriptableObject
{
    const string PATH = "Pipeline/Pipelines";

    public static Pipelines Instance
    {
        get
        {
            var file = Resources.Load(PATH);
            Debug.Assert(file != null, $"File not found : {PATH}");
            return (Pipelines)file;
        }
    }

    [SerializeField] Pipeline<ActionDataKeyPair> mPlayerCharacterControllerPipeline;

    public Pipeline<ActionDataKeyPair> PlayerCharacterControllerPipeline
        => Pipeline<ActionDataKeyPair>.CopyTo(mPlayerCharacterControllerPipeline);

    [SerializeField] Pipeline<AbilityCollision> mAbilityPipeline;

    public Pipeline<AbilityCollision> AbilityPipeline
        => Pipeline<AbilityCollision>.CopyTo(mAbilityPipeline);

    [SerializeField] Pipeline<CollisionData> mHitBoxColliderPipeline;

    public Pipeline<CollisionData> HitBoxColliderPipeline
        => Pipeline<CollisionData>.CopyTo(mHitBoxColliderPipeline);
}