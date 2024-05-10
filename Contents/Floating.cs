using System.Collections;
using UnityEngine;

namespace PlatformGame.Character.Movement
{
    [CreateAssetMenu(menuName = "Custom/MovementAction/Floating")]
    public class Floating : MovementAction
    {
        public float power;

        public override void PlayAction(Rigidbody rigid, MonoBehaviour coroutine)
        {
            coroutine.StartCoroutine(DoMove(rigid));
        }

        IEnumerator DoMove(Rigidbody rigid)
        {
            while (true)
            {
                if (rigid.velocity.magnitude == 0)
                {
                    rigid.AddForce(Vector3.up * power);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

    }
}