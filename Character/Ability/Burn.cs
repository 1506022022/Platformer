using UnityEngine;

namespace PlatformGame.Character.Combat
{
    public class Burn : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Fire")) return;
            var obj = Instantiate(other.gameObject);
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}