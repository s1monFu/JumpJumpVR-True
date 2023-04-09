using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    public float speed = 1f; // speed of movement
    public KeyCode moveKey = KeyCode.Space; // key to trigger movement

    private bool isMoving = false; // flag to indicate if object is currently moving
    private Vector3 targetPosition; // target position to move towards
    private GameObject[] blocks; // array to store all blocks
    private Renderer blockRenderer; // reference to the block's renderer component

    void Start()
    {
        blocks = new GameObject[3];
        blocks[0] = GameObject.FindGameObjectWithTag("Block1");
        blocks[1] = GameObject.FindGameObjectWithTag("Block2");
        blocks[2] = GameObject.FindGameObjectWithTag("Block3");
        blockRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(moveKey) && !isMoving)
        {
            GameObject blockToMove = GetBlockWithSmallestZ();

            Vector3 prevBlockPosition = Vector3.zero;
            if (blockToMove != blocks[0])
            {
                prevBlockPosition = blocks[Array.IndexOf(blocks, blockToMove) - 1].transform.position;
            }

            float distanceToMove = blockToMove.transform.position.z;
            if (blockToMove != blocks[0])
            {
                distanceToMove -= prevBlockPosition.z;
            }

            targetPosition = blockToMove.transform.position + Vector3.back * distanceToMove;
            isMoving = true;

            MoveBlocks();
        }

        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;

                if (transform.position.z < 0.2)
                {
                    if (gameObject == GetBlockWithSmallestZ())
                    {
                        float randomZ = UnityEngine.Random.Range(15, 20);
                        transform.position = new Vector3(transform.position.x, transform.position.y, randomZ);
                        blockRenderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
                    }
                }

                ReorderBlocks();
            }
        }
    }

    GameObject GetBlockWithSmallestZ()
    {
        GameObject blockToMove = blocks[0];
        for (int i = 1; i < blocks.Length; i++)
        {
            if (blocks[i].transform.position.z < blockToMove.transform.position.z)
            {
                blockToMove = blocks[i];
            }
        }
        return blockToMove;
    }

    void MoveBlocks()
    {
        for (int i = 1; i < blocks.Length; i++)
        {
            GameObject blockToMove = blocks[i];
            Vector3 prevBlockPosition = blocks[i - 1].transform.position;

            float distanceToMove = blockToMove.transform.position.z - prevBlockPosition.z;

            Vector3 targetPos = blockToMove.transform.position;
            targetPos += Vector3.back * distanceToMove;

            blockToMove.GetComponent<block>().targetPosition = targetPos;
            blockToMove.GetComponent<block>().isMoving = true;
        }
    }

    void ReorderBlocks()
    {
        Array.Sort(blocks, (x, y) => x.transform.position.z.CompareTo(y.transform.position.z));
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].transform.SetSiblingIndex(i);
        }
    }
}
