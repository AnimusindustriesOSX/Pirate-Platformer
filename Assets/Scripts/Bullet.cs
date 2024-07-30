using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPlayerBullet;
    public float speed = 5f;
    public int damage = 10;
    public float lifetime = 5f;
    private Animator animator;

    void Start(){
        Destroy(gameObject, lifetime);
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        // Move the bullet forward in its local direction
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    public void Impact(){
        speed = 0;
        if(animator)animator.SetBool("impact",true);
    }

}