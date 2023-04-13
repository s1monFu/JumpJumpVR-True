using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDetectorScript : MonoBehaviour
{

    public GameObject objectToSpawn;
    public Transform spawnPosition;
    public float minDelayTime = 1f;
    public float maxDelayTime = 2f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected");
        if (other.gameObject.CompareTag("Block"))
        {
            Destroy(other.gameObject);
            float delayTime = Random.Range(minDelayTime, maxDelayTime);
            Invoke("SpawnObject", delayTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObject()
    {
        Instantiate(objectToSpawn, spawnPosition.position, Quaternion.identity);
    }
}
