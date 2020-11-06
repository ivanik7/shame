using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public float speed = 3f;

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(speed, 0);
        transform.Translate(movement * Time.deltaTime);
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
