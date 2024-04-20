using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public Vector2 mLifeRange;
    float mlife;
    private void Awake()
    {
        Debug.Assert(mLifeRange.x <= mLifeRange.y);
        mlife = Random.Range(mLifeRange.x,mLifeRange.y);
    }
    void Update()
    {
        mlife -= Time.deltaTime;
        if(mlife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
