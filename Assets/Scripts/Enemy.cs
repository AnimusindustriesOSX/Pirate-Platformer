using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int collisionDamage= 1;

    void Start()
    {
        currentHealth = maxHealth;
        
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
        // Handle enemy death
        Destroy(gameObject);
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
}