using UnityEngine;

public class Bird : Unit
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cat cat = collision.GetComponent<Cat>();
        if (cat)
        {
            cat.Eat();
            Die();
        }
    }
}
