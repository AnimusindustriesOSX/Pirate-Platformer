using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int shadowCollisionDamage = 0;
    public int collisionDamage = 0;
    
    

    Animator animator;
    enemyAI enemyAI;
    BoxCollider2D boxCollider2D;
    void Start()
    {
        
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<enemyAI>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {   
        animator.SetBool("Dead",true);
        enemyAI.enabled = false;
        boxCollider2D.enabled = false;
        StartCoroutine(Wait(0.5f));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.CompareTag("MainWeapon"))
        {
            if (gameObject.CompareTag("Physical")){
                TakeDamage(other.gameObject.GetComponent<Sword>().damage);
            }    
        }else if(other.gameObject.CompareTag("Player-shadow-attack")){
            if (gameObject.CompareTag("Shadow")){
                //currentHealth -= other.gameObject.GetComponent<ShadowAttack>().damage;
            }
        }
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}