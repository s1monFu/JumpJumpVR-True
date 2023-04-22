using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveBack : MonoBehaviour
{
    public float speed = 0.5f;
    //public string scene_name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.z > -6f && transform.position.z < 42f)
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }

        if( transform.position.z >= 40f ){
            SceneManager.LoadScene("YOU_WIN");
        }

    }
}
