using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    public Animator swordAnimator;

    
    GameObject mainWeapon;
    // Start is called before the first frame update
    void Start()
    {
        TorsoAnimator = GameObject.Find("body").GetComponent<Animator>();
        LegAnimator = GameObject.Find("legs").GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
        rb = GetComponent<Rigidbody2D>();
        mainWeapon = GameObject.Find("MainWeapon");
        swordAnimator = mainWeapon.GetComponent<Animator>();
        HP = MaxHP;
        insanity=0;
    }

    // Update is called once per frame
    void Update()
    {   
        //physical interaction
        if(Input.GetKeyDown(KeyCode.E)){
            closestItem = FindClosestHarvestableItem();
            if(closestItem != null &&  Vector3.Distance(transform.position, closestItem.transform.position) <= pickupDistance){
                
                if (inventory != null && inventory.AddItem(closestItem.GetComponent<ItemPickup>().item))
                {
                    transform.GetComponent<Audio>().playSound();
                    closestItem.GetComponent<Regrow>().Harvest();
                }
            } 
        }

        //shadow interaction
        if(Input.GetKeyDown(KeyCode.Q)){
            closestItem = FindClosestHarvestableItem();
            if(closestItem != null &&  Vector3.Distance(transform.position, closestItem.transform.position) <= pickupDistance){
                GameObject shadow = FindGameObjectInChildWithTag(closestItem,"Shadow");
                Item shadowItem = shadow.GetComponent<ItemPickup>().item;
                if (inventory != null && inventory.AddItem(shadowItem)){
                     transform.GetComponent<Audio>().playSound();
                    closestItem.GetComponent<Regrow>().Harvest();
                }
            } 
        }
    

        /**
        if(Input.GetMouseButton(0)){
            //attack
            
            autoCooldown = true;
        }
        if(autoCooldown){
            autoCooldown = false;
            mainWeapon.transform.eulerAngles = Vector3.zero;
        }
        **/
        if (Input.GetMouseButtonDown(0)) // Check if left mouse button is pressed
        {
            if (swordAnimator != null)
            {
                swordAnimator.SetBool("isAttacking", true); // Set the bool parameter to true
            }
        }
        else if (Input.GetMouseButtonUp(0)) // Check if left mouse button is released
        {
            if (swordAnimator  != null)
            {
                swordAnimator.SetBool("isAttacking", false); // Set the bool parameter to false
            }
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
            InsanityChangeSigned(other.GetComponentInParent<Enemy>().shadowCollisionDamage);//other.gameObject.GetComponent<Enemy>().shadowCollisionDamage);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {   
        rb.velocity = Vector2.zero;
        HP -= 10;
        if(collision.gameObject.CompareTag("Physical-attack")){
            HP -= collision.gameObject.GetComponent<Enemy>().collisionDamage ;
        }else if(collision.gameObject.CompareTag("Shadow")){ 
            InsanityChangeSigned(2);
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

    GameObject FindClosestHarvestableItem()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        GameObject closestItem = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject item in items)
        {
            ItemPickup itemPickup = item.GetComponent<ItemPickup>();
            if(itemPickup.GetEnable()){
                Debug.Log("itemPickup.GetEnable() == true");
                float distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance < closestDistance && item.GetComponent<ItemPickup>().GetEnable() )
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
            
        }
        Debug.Log(closestItem);
        return closestItem;

    }

    public void InsanityChangeSigned(int insanityChange){
        insanity += insanityChange;
        if(insanity > 200){insanity=200;} 
        if(insanity < 0){insanity=0;} 
    }
}
