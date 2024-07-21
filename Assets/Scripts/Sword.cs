using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage = 10;
    public const float cooldown = 1;  
   /*  void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Physical"))
        {
            // Deal damage to the enemy

            
            if (other.GetComponent<Enemy>() != null)
            {
                other.GetComponent<Enemy>().TakeDamage(damage);
                Debug.Log("Physical Collided with sword");
            }
        }
    } */
}
