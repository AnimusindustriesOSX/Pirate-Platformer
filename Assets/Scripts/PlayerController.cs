using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    
    
    [Header("Attribute Caps")]
    public float MaxHP = 100;
    public float MaxInsanity = 100;
    
    [Header("Attributes")]
    public int strength = 10;
    public float HP;
    public float insanity;
    
    [Header("Item Pickup")]
    public float pickupDistance = 3;
    public Vector2 direction;
    public float speed;
    public GameObject closestItem;
    private Inventory inventory;
    private Rigidbody2D rb;
    private Animator LegAnimator;
    private Animator TorsoAnimator;
    public int selectedItemSlot;
    
     [Header("Combat")]
    public Animator swordAnimator;
    public Item selectedItem;
    public bool ShadowShieldUp = false;
    

    
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
        selectedItem = null;
    }

    // Update is called once per frame
    void Update()
    {   
        AttributeOverflow();
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


        if(Input.GetKeyDown(KeyCode.Alpha1)){
            try{
                ItemListing itemListing = inventory.getItemListingByID(21);
                if(itemListing != null && itemListing.amount>=1){
                    selectedItem = itemListing.item;
                    selectedItemSlot = 1;
                    ConsumablesCanvas.updateSelectedConsumable(selectedItemSlot);
                }
            } 
            catch (System.Exception e)
            {
                Debug.Log("Can't select item you don't have: " + e.Message);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            try{
                ItemListing itemListing = inventory.getItemListingByID(22);
                if (itemListing != null && itemListing.amount >= 1){
                    selectedItem = itemListing.item;
                    selectedItemSlot = 2;
                    ConsumablesCanvas.updateSelectedConsumable(selectedItemSlot);
                }
                else
                {
                    Debug.Log("Item not found or insufficient amount");
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("Can't select item you don't have: " + e.Message);
            }
        }
    
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            try{
                ItemListing itemListing = inventory.getItemListingByID(23);
                if(itemListing != null && itemListing.amount>=1){
                    selectedItem = itemListing.item;
                    selectedItemSlot = 3;
                    ConsumablesCanvas.updateSelectedConsumable(selectedItemSlot);
                }
            } catch (System.Exception e)
            {
                Debug.Log("Can't select item you don't have: " + e.Message);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            try{
                ItemListing itemListing = inventory.getItemListingByID(24);
                if(itemListing != null && itemListing.amount>=1){
                    selectedItem = itemListing.item;
                    selectedItemSlot = 4;
                    ConsumablesCanvas.updateSelectedConsumable(selectedItemSlot);
                }
            } catch (System.Exception e)
            {
                Debug.Log("Can't select item you don't have: " + e.Message);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            try{
                ItemListing itemListing = inventory.getItemListingByID(31);
                if(itemListing != null && itemListing.amount>=1){
                    selectedItem = itemListing.item;
                    selectedItemSlot = 5;
                    ConsumablesCanvas.updateSelectedConsumable(selectedItemSlot);
                }
            } catch (System.Exception e)
            {
                Debug.Log("Can't select item you don't have: " + e.Message);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)){
            try{
                ItemListing itemListing = inventory.getItemListingByID(32);
                if(itemListing != null && itemListing.amount>=1){
                    selectedItem = itemListing.item;
                    selectedItemSlot = 6;
                    ConsumablesCanvas.updateSelectedConsumable(selectedItemSlot);
                }
            } catch (System.Exception e)
            {
                Debug.Log("Can't select item you don't have: " + e.Message);
            }
        }
    
        if (Input.GetKeyDown(KeyCode.Space)){
            if(selectedItem!=null){
                useItem(selectedItem);
            }
        }
    

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
            Debug.Log("COLLIDED WITH PLAYER + = INSANITY");
            if(ShadowShieldUp){
                insanity += other.GetComponentInParent<Enemy>().shadowCollisionDamage/2;
            }else{
                insanity += other.GetComponentInParent<Enemy>().shadowCollisionDamage;
            }
            HP -= other.GetComponentInParent<Enemy>().collisionDamage;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {   
        rb.velocity = Vector2.zero;
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
            if(item.GetComponent<ItemPickup>()!= null){
                if(item.GetComponent<ItemPickup>().GetEnable()){
                
                    float distance = Vector3.Distance(transform.position, item.transform.position);
                    if (distance < closestDistance && item.GetComponent<ItemPickup>().GetEnable() )
                    {
                        closestDistance = distance;
                        closestItem = item;
                    }
                }
            }
        }
        return closestItem;
    }

    public void InsanityChangeSigned(int insanityChange){
        insanity += insanityChange;
        if(insanity > 100){insanity=100;} 
    }  
    public void AttributeOverflow(){
        
        if(insanity > MaxInsanity){insanity=MaxInsanity;} 
        if(insanity < 0){insanity=0;} 
        if(HP > MaxHP){HP=MaxHP;} 
        if(HP < 0){HP=0;} 
    }

    public void useItem(Item item){
        if(inventory.ReduceItem(item)){
            item.Effect();
            Debug.Log("ITEM USED" + item);
        }
    }

    public void healHP(int HPHealed){
        HP += HPHealed;
        if(HP > MaxHP){HP = MaxHP;}
    }

    
    
}
