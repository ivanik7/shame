using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 60.0f;
    public float fallMultiplyer = 2.5f;
    public float jumpMultiplyer = 2.0f;
    public bool holdJump = false;
    public bool isGrounded = true;

    private Rigidbody2D rigibody;


    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        // Правильный прыжек
        // TODO: Пересмотреть этот кусок кода
        if (rigibody.velocity.y < 0)
        {
            rigibody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplyer - 1) * Time.deltaTime;
        }
        else if (rigibody.velocity.y > 0 && !holdJump)
        {
            rigibody.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplyer - 1) * Time.deltaTime;
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
}
