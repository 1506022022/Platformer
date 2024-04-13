using UnityEngine;

public class Shooter : MonoBehaviour
{
    float mDelay;
    [SerializeField] GameObject bullet;
    GameObject instance;

    void Update()
    {
        if (Time.time > mDelay)
        {
            Destroy(instance);
            mDelay = Time.time + 3f;
            instance = Instantiate(bullet);
            instance.transform.position = gameObject.transform.position;
            instance.GetComponent<Rigidbody>().AddForce(Vector3.left * 150f);
        }
    }
}
