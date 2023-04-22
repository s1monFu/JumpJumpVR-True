using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public Transform resetPosition;
    public Camera playerCamera;

    public AudioClip toneClip;
    private AudioSource toneSource;

    void Start()
    {
        toneSource = GetComponent<AudioSource>();
        toneSource.clip = toneClip;
    }

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

            toneSource.PlayOneShot(toneClip);
        }
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
