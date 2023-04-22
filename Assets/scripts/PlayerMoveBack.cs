using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveBack : MonoBehaviour
{
    public float speed = 0.5f;

    void Update()
    {
        if (transform.position.z > -6f && transform.position.z < 42f)
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }

        if( transform.position.z >= 42f && transform.position.y >= -3f){
            Invoke("WIN", 2);
        }
    }

    void WIN()
    {
        SceneManager.LoadScene("YOU_WIN");
    }
}
