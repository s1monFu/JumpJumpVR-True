using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    public float speed = 1f; // speed of movement
    public float distance = 3f; // distance to move along z-axis
    public KeyCode moveKey = KeyCode.Space; // key to trigger movement

    private bool isMoving = false; // flag to indicate if object is currently moving
    private Vector3 targetPosition; // target position to move towards

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(moveKey))
        {
            // set target position to current position plus movement distance along z-axis
            targetPosition = transform.position + Vector3.back * distance;
            isMoving = true;
        }

        if (isMoving)
        {
            // move towards target position smoothly using Lerp function
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

            // check if we have reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}
