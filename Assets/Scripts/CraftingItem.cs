using UnityEngine;

// Class representing a single crafting item
public class CraftingItem : MonoBehaviour
{
    public string itemName; // Name of the item, e.g., "Mushroom", "Apple"
    public int ID; // Unique ID for each item type
    public Item item; // Reference to the corresponding inventory item

    // Method to initialize a CraftingItem with its corresponding Item
    public void Initialize(Item newItem)
    {
        item = newItem;
        itemName = newItem.Name;
        ID = newItem.ID;
    }
}
