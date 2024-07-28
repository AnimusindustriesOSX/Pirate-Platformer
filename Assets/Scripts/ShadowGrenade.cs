using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGrenade : MonoBehaviour
{
    public GameObject player;
    public Vector3 targetPosition;
    public string spritePath = "shadowGrenade";
    SpriteRenderer spriteRenderer;
    float startTime;
    GameObject shadowCursor;
    Vector3 startingPosition;
    private float journeyLength;
    public float speed = 30 ;
    private Collider2D [] colliders;
    public float radius =3;
    public int damage = 100;
    // Start is called before the first frame update
    void Start()
    {
        shadowCursor = GameObject.Find("Shadow_Cursor");
        spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite loadedSprite = Resources.Load<Sprite>(spritePath);
        spriteRenderer.sprite = loadedSprite;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        
        startingPosition = transform.position;
        targetPosition = shadowCursor.transform.position;
        journeyLength = Vector3.Distance(startingPosition, targetPosition);
        
        startTime = Time.time;
    }

    private void Update() {
        /* if(Time.time - startTime < 1){
            transform.position = startingPosition + targetPosition*Time.fixedDeltaTime;
        } */
      
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startingPosition, targetPosition, fractionOfJourney);
        if (fractionOfJourney >= 1.0f)
        {
            StartCoroutine(OnLerpFinished());
        }
    }


    IEnumerator OnLerpFinished()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);       
            foreach (Collider2D collider in colliders)
            {
                if(collider.transform.CompareTag("Shadow")){
                    Enemy enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        Debug.Log("Collided with Enemy: " + collider.name);
                        // Additional logic for interacting with the Enemy can be added here
                        enemy.TakeDamage(damage);
                    }
                }
            }
            return null;
    }
    
}
