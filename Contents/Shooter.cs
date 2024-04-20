using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject mBullet;
    [SerializeField] Vector3 mDir;
    [SerializeField] float mPower;
    [SerializeField] float mDelay;
    float nextTime;
    void Update()
    {
        if (nextTime <= Time.time)
        {
            nextTime = Time.time + mDelay;
            var instance = Instantiate(mBullet);
            instance.transform.position = gameObject.transform.position;
            instance.transform.rotation= Quaternion.identity;
            instance.GetComponent<Rigidbody>().AddForce(mDir * mPower);
        }
    }
}
