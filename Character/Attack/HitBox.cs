using UnityEngine;

public class HitBox : MonoBehaviour
{
    GameObject attacker;
    int tick;
    void OnTriggerEnter(Collider other)
    {
        var rigid = other.GetComponent<Rigidbody>();
        if (rigid != null && rigid.velocity.magnitude > 0f)
        {
            Vector3 dir = Vector3.zero;
            dir.x = rigid.velocity.x < 0 ? -1 : 1;
            dir.y = 3;
            rigid.AddForce(dir * 300f);
        }
    }
    void OnEnable()
    {
        tick = 0;
    }
    void Update()
    {
        if (tick > 3)
        {
            gameObject.SetActive(false);
        }
        tick++;
    }
}
