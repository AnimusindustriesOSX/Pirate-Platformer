using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float pickupDistance = 3;
    private Vector2 _input;
    private Vector3 direction;
    [SerializeField] private float speed;
    private Inventory inventory;
    private Item[] nearbyItems;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT
        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (direction != Vector3.zero)
        {

            gameObject.transform.position += speed * Time.deltaTime * direction;
            if(direction.x < 0) {
                transform.localScale = new Vector3(1,1,1);
            }
            else if(direction.x > 0){
                transform.localScale = new Vector3(-1,1,1);
            } 
        }

        //ITEMS
        if(Input.GetKeyDown(KeyCode.E)){
            GameObject closestItem = FindClosestItemWithTag("Item");
            if(closestItem != null &&  Vector3.Distance(transform.position, closestItem.transform.position) <= pickupDistance){
                if (inventory != null && inventory.AddItem(closestItem.GetComponent<ItemPickup>().item))
                {
                    Destroy(closestItem);
                }
            } 
        }
        
        
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
