using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPotion : MonoBehaviour
{
    private float startTime;
    public int duration = 10;
    public int dmg = 10;
    public int radius = 5;
    private Collider2D [] colliders;
    GameObject shadowDisc;


    void Start()
    {
        shadowDisc = GameObject.Find("Large_Player_Shadow_Disc");
        startTime = Time.time;
        shadowDisc.GetComponent<ShadowDisc>().SpriteChange(1);
        StartCoroutine(CheckCollisions());
    }

    void Update()
    {
        if(Time.time - startTime > duration){
            shadowDisc.GetComponent<ShadowDisc>().SpriteChange(0);
            StopCoroutine(CheckCollisions());
            Destroy(GetComponent<LightPotion>());
        }
    }

    IEnumerator CheckCollisions()
    {
        while (true)
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            
            foreach (Collider2D collider in colliders)
            {
                // Check if the collider's GameObject has an Enemy component
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Debug.Log("Collided with Enemy: " + collider.name);
                    // Additional logic for interacting with the Enemy can be added here
                    enemy.TakeDamage(10);
                }
            }
            yield return new WaitForSeconds(0.5f); 
        }
    }
}
