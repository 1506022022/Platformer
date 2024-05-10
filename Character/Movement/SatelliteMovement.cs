using System.Collections;
using UnityEngine;

namespace PlatformGame.Character.Movement
{
    [CreateAssetMenu(menuName = "Custom/MovementAction/Satellite")]
    public class SatelliteMovement : MovementAction
    {
        public float mSpeed = 1f;
        public float mRadius = 5f;

        public override void PlayAction(Rigidbody rigid, MonoBehaviour coroutine)
        {
            var satellite = coroutine.transform;
            coroutine.StartCoroutine(AroundTarget(satellite));
        }

        public override void StopAction(Rigidbody rigid, MonoBehaviour coroutine)
        {
            var owner = rigid.transform.parent;
            var satellite = coroutine.transform;
            coroutine.StopCoroutine(AroundTarget(satellite));
        }

        protected virtual IEnumerator AroundTarget(Transform satellite)
        {
            float angle = 0f;
            while (true)
            {
                angle += Time.deltaTime * mSpeed;
                if (angle < 360)
                {
                    var rad = Mathf.Deg2Rad * (angle);
                    var x = mRadius * Mathf.Sin(rad);
                    var y = mRadius * Mathf.Cos(rad);
                    satellite.localPosition = new Vector3(x, 1, y);
                }
                else
                {
                    angle = 0;
                }
                yield return null;
            }
        }

    }
}
