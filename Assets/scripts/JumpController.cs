using UnityEngine;
using UnityEngine.UI;

public class JumpController : MonoBehaviour
{
    public float forceMultiplier = 10f;
    public float maxHoldTime = 2f;
    public float forwardForceMultiplier = 5f;

    private Rigidbody rb;
    private bool isJumping = false;
    public float holdTime = 0f;

    public float speed = 0.5f;

    public Slider slider;

    private Camera MainCamera;
    float InitialCameraYPos;
    float CurrentCameraYPos;
    float CameraYPosOffset;
    public float DownThreshold;
    public float UpThreshold;

    public AudioClip toneClip;
    public float toneVolume = 10f;
    private AudioSource toneSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        InitialCameraYPos = MainCamera.transform.position.y;

        Debug.Log("Camera Y position: " + InitialCameraYPos);

        toneSource = GetComponent<AudioSource>();
        toneSource.clip = toneClip;
    }

    void Update()
    {
        CurrentCameraYPos = MainCamera.transform.position.y;
 
        CameraYPosOffset = InitialCameraYPos - CurrentCameraYPos;
        Debug.Log("Camera Y Offset: " + CameraYPosOffset);


        if ((Input.GetKeyDown(KeyCode.Space) || CameraYPosOffset > DownThreshold) && !isJumping)
        {
            // Start playing the tone from the beginning
            toneSource.Stop();
            toneSource.PlayOneShot(toneClip);
            isJumping = true;
            holdTime = 0f;
        }
        if ((Input.GetKey(KeyCode.Space) || CameraYPosOffset > DownThreshold) && isJumping)
        {
            if (holdTime <= maxHoldTime) {
                holdTime += Time.deltaTime;
                slider.value = holdTime / maxHoldTime;
                float volume = holdTime / maxHoldTime * toneVolume;
                Debug.Log("Volume: " + volume);
                toneSource.volume = volume;
            }
        }
        if ((Input.GetKeyUp(KeyCode.Space) || CameraYPosOffset < UpThreshold) && isJumping)
        {
            toneSource.Stop();
            float force = Mathf.Clamp(holdTime * forceMultiplier, 0f, forceMultiplier * maxHoldTime);
            float forwardForce = Mathf.Clamp(holdTime * forwardForceMultiplier, 0f, forwardForceMultiplier * maxHoldTime);
            Vector3 jumpForce = transform.forward * forwardForce + Vector3.up * force;
            rb.AddForce(jumpForce, ForceMode.Impulse);
            isJumping = false;
            holdTime = 0f;
            slider.value = 0f;
        }
    }
}
