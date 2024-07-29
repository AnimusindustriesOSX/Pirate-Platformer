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
    public bool destinationReached = false;
    float fractionOfJourney;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
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
        if (!destinationReached){
            float distCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startingPosition, targetPosition, fractionOfJourney);
        }
        
        if (fractionOfJourney >= 1.0f){
            destinationReached = true;
            startTime = Time.time;
            StartCoroutine(Boom());
            fractionOfJourney = 0;
            }
        }

    IEnumerator Boom(){
        startTime = Time.time;
        while(Time.time - startTime < 0.1f){
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one*30, 0.1f);
                Debug.Log("after lerp:" + Time.time);
                yield return null;
        }
        Debug.Log("reached boom");
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
            Destroy(gameObject);
    }
    
}
