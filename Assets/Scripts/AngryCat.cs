using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AngryCat : Unit
{
    public float speed = 2f;

    private float raduis = 0.1f;
    private float direction = 1f;

    private SpriteRenderer sprite;

    public void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.right * direction * 0.5F, raduis);

        if (colliders.Length > 0) direction *= -1.0F;

        Vector2 movement = new Vector2(speed * direction, 0);
        transform.Translate(movement * Time.deltaTime);

        sprite.flipX = direction > 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cat cat = collision.GetComponent<Cat>();
        if (cat)
        {
            cat.ReceiveDamage();
        }
    }

}
