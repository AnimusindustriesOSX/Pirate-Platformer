using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public const int MaxHP = 100;
    public int strength = 10;
    public int HP;
    public int insanity;
    public float pickupDistance = 3;
    private Vector2 direction;
    [SerializeField] private float speed;
    public GameObject closestItem;
    private Inventory inventory;
    private Rigidbody2D rb;
    private Animator LegAnimator;
    private Animator TorsoAnimator;
    bool autoCooldown = false;
    GameObject mainWeapon;
    // Start is called before the first frame update
    void Start()
    {
        TorsoAnimator = GameObject.Find("body").GetComponent<Animator>();
        LegAnimator = GameObject.Find("legs").GetComponent<Animator>();
        
        inventory = GetComponent<Inventory>();
        rb = GetComponent<Rigidbody2D>();
        mainWeapon = GameObject.Find("MainWeapon");
        HP = MaxHP;
        insanity = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        //physical interaction
        if(Input.GetKeyDown(KeyCode.E)){
            closestItem = FindClosestItemWithTag("Item");
            if(closestItem != null &&  Vector3.Distance(transform.position, closestItem.transform.position) <= pickupDistance){
                if (inventory != null && inventory.AddItem(closestItem.GetComponent<ItemPickup>().item))
                {
                    Debug.Log("item");
                    transform.GetComponent<Audio>().playSound();
                    closestItem.GetComponent<Regrow>().Harvest();
                }
            } 
        }

        //shadow interaction
        if(Input.GetKeyDown(KeyCode.Q)){
            closestItem = FindClosestItemWithTag("Item");
            if(closestItem != null &&  Vector3.Distance(transform.position, closestItem.transform.position) <= pickupDistance){
                GameObject shadow = FindGameObjectInChildWithTag(closestItem,"Shadow");
                Item shadowItem = shadow.GetComponent<ItemPickup>().item;
                if (inventory != null && inventory.AddItem(shadowItem)){
                    Debug.Log("Shadow item");
                    closestItem.GetComponent<Regrow>().Harvest();
                }
            } 
        }
        
        if(Input.GetMouseButton(0)){
            //attack
            
            autoCooldown = true;
        }
        if(autoCooldown){
            autoCooldown = false;
            mainWeapon.transform.eulerAngles = Vector3.zero;
        }
        
    }

    private void FixedUpdate() {
        //MOVEMENT
        Vector2 input =  new Vector2(Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical"));
        if(input != Vector2.zero){
            TorsoAnimator.SetBool("Walking", true);
            LegAnimator.SetBool("Walking", true);
            direction = input.normalized;
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * direction);
            if(direction.x < 0) {
                transform.localScale = new Vector3(1,1,1);
            }
            else if(direction.x > 0){
                transform.localScale = new Vector3(-1,1,1);
            } 
        }else{
            TorsoAnimator.SetBool("Walking", false);
            LegAnimator.SetBool("Walking", false);
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Shadow")){
            Debug.Log("shadow collided");
            insanity += 1 ;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {   
        
        Debug.Log(collision.gameObject.tag);
        rb.velocity = Vector2.zero;
        HP -= 10;
        if(collision.gameObject.CompareTag("Physical-attack")){
            HP -= collision.gameObject.GetComponent<Enemy>().collisionDamage ;
        }else if(collision.gameObject.CompareTag("Shadow")){ 
            Debug.Log("shadow");
            insanity += 10 ;
            HP -= 10 ;
        }
    }

    public static GameObject FindGameObjectInChildWithTag (GameObject parent, string tag)
	{
		Transform t = parent.transform;

		for (int i = 0; i < t.childCount; i++) 
		{
			if(t.GetChild(i).gameObject.tag == tag)
			{
				return t.GetChild(i).gameObject;
			}
				
		}
			
		return null;
	}
    GameObject FindClosestItemWithTag(string tag)
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestItem = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject item in items)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestItem = item;
            }
        }
        return closestItem;
    }

    
}
