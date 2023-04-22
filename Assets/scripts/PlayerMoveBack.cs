using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBack : MonoBehaviour
{
    public float speed = 0.5f;
    public GameOverUI gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    void Update()
    {
        if (transform.position.z > -6f && transform.position.z < 42f)
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        else if( transform.position.z < 42f ){
            gameOverUI.ShowGameOverPanel();
        }

    }
}
