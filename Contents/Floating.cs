using UnityEngine;

public class Floating : MonoBehaviour
{
    Rigidbody mRigid;
    public float power;
    void Start()
    {
        mRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mRigid.velocity.magnitude == 0)
        {
            mRigid.AddForce(Vector3.up * power);
        }
    }
}
