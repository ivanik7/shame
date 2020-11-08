using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Unit
{
    public float speed = 9.5f;
    public Cat target;
    public float yBorder = -5;

    private float raduis = 0.1f;

    private float direction = 1f;
    public float jumpCooldown = 0.5f;
    private float currentJumpCooldown = 0f;

    private Rigidbody2D rigibody;
    private Animator animator;
    private SpriteRenderer sprite;
    private Jump jump;

    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        jump = GetComponentInChildren<Jump>();
    }
    void Update()
    {
        if (transform.position.y < yBorder) Respawn();

        // Смотрим в какой стороне кот
        var delta = target.transform.position.x - transform.position.x;
        if (Mathf.Abs(delta) > 2)
        {
            direction = (delta > 0 ? 1f : -1f);
        }

        if (jumpCooldown > 0f)
        {
            currentJumpCooldown -= Time.deltaTime;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.right * direction, raduis);
        bool isCollided = false;
        foreach (Collider2D item in colliders)
        {
            if (item.name != this.name)
            {
                isCollided = true;
            }
        }

        jump.holdJump = isCollided;
        if (isCollided)
        {
            Debug.Log("dsads");
            if (currentJumpCooldown <= 0f)
            {
                currentJumpCooldown = jumpCooldown;
                jump.DoJump();
            }
        }

        sprite.flipX = direction < 0;

        // Двигаемя
        Vector2 movement = new Vector2(speed, 0);
        transform.Translate(direction * movement * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cat cat = collision.GetComponent<Cat>();
        if (cat)
        {
            cat.ReceiveDamage();
        }
    }

    private void Respawn()
    {
        var respawn = GameObject.Find("Respawn");
        transform.position = respawn.transform.position;
    }

}
