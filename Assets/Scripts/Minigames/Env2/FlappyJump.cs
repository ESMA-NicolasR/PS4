using UnityEngine;

public class RhythmJump : MonoBehaviour
{
    public float minJumpForce = 3f;
    public float maxJumpForce = 8f;
    public float maxDelay = 1.5f; 

    private Rigidbody2D rb;
    private float lastJumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastJumpTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float timeSinceLastJump = Time.time - lastJumpTime;
            lastJumpTime = Time.time;

            float t = Mathf.Clamp01(1 - (timeSinceLastJump / maxDelay));
            float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, t);

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
