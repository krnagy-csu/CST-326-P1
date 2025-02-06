using UnityEngine;

public class PlinkoScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -8)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x,1.5f,gameObject.transform.position.z);
                rb.linearVelocity = new Vector3(rb.linearVelocity.x,-1.0f,rb.linearVelocity.z);

            }
    }
    
    void OnTriggerEnter (Collider other){
        Debug.Log($"Entered {other.name}");
        Destroy(gameObject);
    }
}
