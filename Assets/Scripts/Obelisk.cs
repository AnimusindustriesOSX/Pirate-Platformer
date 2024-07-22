using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Obelisk : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Spawning spawning;
    public SpriteRenderer obeliskShadow;
    // Start is called before the first frame update
    void Start()
    {
        spawning = GetComponent<Spawning>();
        currentHealth = maxHealth;
        obeliskShadow = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {   
        
        Color tmp = obeliskShadow.color;
        tmp.a = 1f-((float)(maxHealth - currentHealth)/(float)maxHealth) ;
        obeliskShadow.color = tmp;
    }

     public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            spawning.enabled = false;
            Die();
        }
    }

    void Die()
    {
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
