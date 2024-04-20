using UnityEngine;

public class Twist : MonoBehaviour
{
    public Vector3 rotate;
    public float scale;
    public float height;
    private void Awake()
    {
        height = 0f;
    }
    void Update()
    {
        transform.Rotate(rotate);
        height += 1 * Time.deltaTime;
        transform.localScale = Vector3.one + Vector3.one * height * scale;
    }
}
