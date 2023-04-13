using UnityEngine;
using UnityEngine.UI;

public class JumpController : MonoBehaviour
{
    public float forceMultiplier = 10f;
    public float maxHoldTime = 2f;
    public float forwardForceMultiplier = 5f;

    private Rigidbody rb;
    private bool isJumping = false;
    private float holdTime = 0f;

    public float speed = 0.5f;

    public Slider slider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            holdTime = 0f;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (holdTime <= maxHoldTime) {
                holdTime += Time.deltaTime;
                slider.value = holdTime / maxHoldTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
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
