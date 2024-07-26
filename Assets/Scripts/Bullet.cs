using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;

    void Update()
    {
        // Move the bullet forward in its local direction
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Add collision logic here (e.g., destroy bullet and damage player/enemy)
        Destroy(gameObject);
    }
}
