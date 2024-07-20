using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new();
    public int inventorySize = 20;

    public bool AddItem(Item item)
    {
        if (items.Count < inventorySize)
        {
            items.Add(item);
            Debug.Log("Item added: " + item.itemName);
            return true;
        }
        else
        {
            Debug.Log("Inventory is full!");
            return false;
        }
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("Item removed: " + item.itemName);
        }
    }
}
