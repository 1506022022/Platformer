using UnityEngine;

public class Burn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fire")
        {
            var obj = Instantiate(other.gameObject);
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
