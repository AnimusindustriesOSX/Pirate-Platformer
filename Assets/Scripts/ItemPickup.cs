using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // The item to be picked up
    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Inventory inventory = player.GetComponent<Inventory>();
            if (inventory != null && inventory.AddItem(item))
            {
                
            }
        }
    }
}
