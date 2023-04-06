using UnityEngine;

public class JumpController : MonoBehaviour
{
    public float forceMultiplier = 10f;
    public float maxHoldTime = 2f;
    public float forwardForceMultiplier = 5f;

    private Rigidbody rb;
    private bool isJumping = false;
    private float holdTime = 0f;

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
            holdTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            float force = Mathf.Clamp(holdTime * forceMultiplier, 0f, forceMultiplier * maxHoldTime);
            float forwardForce = Mathf.Clamp(holdTime * forwardForceMultiplier, 0f, forwardForceMultiplier * maxHoldTime);
            Vector3 jumpForce = transform.forward * forwardForce + Vector3.up * force;
            rb.AddForce(jumpForce, ForceMode.Impulse);
            isJumping = false;
        }
    }
}
