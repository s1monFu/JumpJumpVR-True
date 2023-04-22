using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public Transform resetPosition;
    public Camera playerCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dead"))
        {
            Debug.Log("Player is dead");
            Invoke("respawn", 1.7f);

            Material blackMaterial = new Material(Shader.Find("Unlit/Color"));
            blackMaterial.color = Color.black;
            playerCamera.SetReplacementShader(Shader.Find("Unlit/Color"), "RenderType");
            playerCamera.SetReplacementShader(blackMaterial.shader, "RenderType");

            

            //Invoke("turnOffCamera", 0.5f);
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

    
    void turnOffCamera()
    {
        playerCamera.gameObject.SetActive(false);
    }
    void respawn()
    {
        transform.position = resetPosition.position;
        playerCamera.gameObject.SetActive(true);
        StartCoroutine(RemoveReplacementShader(0.1f));
    }

    IEnumerator RemoveReplacementShader(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerCamera.ResetReplacementShader();
    }

}
