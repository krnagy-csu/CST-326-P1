using UnityEngine;

public class CameraPointer : MonoBehaviour
{
    public GameObject ball;
    public float ballFollowMulti; //Lower follows more closely.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(ball.transform.position / ballFollowMulti);
    }
}
