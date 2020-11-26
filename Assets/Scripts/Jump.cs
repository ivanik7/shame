using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 10.0f;
    public float fallMultiplyer = 2.5f;
    public float jumpMultiplyer = 2.0f;
    public bool holdJump = false;
    public bool isGrounded = true;

    private Rigidbody2D rigibody;


    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();

    }
    private void FixedUpdate()
    {
        // Правильный прыжек
        // TODO: Пересмотреть этот кусок кода
        if (rigibody.velocity.y < 0)
        {
            rigibody.gravityScale = fallMultiplyer;
        }
        else if (rigibody.velocity.y > 0 && !holdJump)
        {
            rigibody.gravityScale = jumpMultiplyer;
        }
        else
        {
            rigibody.gravityScale = 1f;
        }

    }

    void OnCollisionStay2D(Collision2D collisionInfo)
    {
        isGrounded = true;

    }
    void OnCollisionExit2D()
    {
        isGrounded = false;
    }

    public void DoJump()
    {
        if (isGrounded)
        {
            rigibody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void SetHold(bool hold)
    {
        holdJump = hold;
    }
}
