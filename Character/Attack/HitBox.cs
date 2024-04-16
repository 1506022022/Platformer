using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{
    int tick;
    public UnityEvent hitedEvent;
    void OnTriggerEnter(Collider other)
    {
        var rigid = other.GetComponent<Rigidbody>();
        if (rigid != null && rigid.velocity.magnitude > 0f)
        {
            Vector3 dir = gameObject.transform.forward; 
            dir.y = 3;
            rigid.AddForce(dir * 300f);
            hitedEvent.Invoke();
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
