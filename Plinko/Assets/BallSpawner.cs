using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    private float counter = 0;
    private float spawnCounter = 0;
    public float range = 1;
    Vector3 spawnPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform siblingTransform = GetComponent<Transform>();
        spawnPos = siblingTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float spawnSpot;
        spawnSpot = Random.Range(range,-range) / 100;
        spawnPos = new Vector3(spawnSpot,spawnPos.y,spawnPos.z);
        counter += Time.deltaTime;
        spawnCounter += Time.deltaTime;
        /*if (counter >= 5){
            GameObject newObj = Instantiate (ballPrefab, spawnPos, Quaternion.identity);
            Debug.Log(newObj.name);
            counter = 0; 
        }*/

        if (Input.GetKey(KeyCode.Space) && spawnCounter > 0.3f){
            GameObject newObj = Instantiate (ballPrefab, spawnPos, Quaternion.identity);
            Debug.Log(newObj.name);
            spawnCounter = 0;
        }
    }
}
