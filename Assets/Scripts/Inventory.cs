using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemListing
{
    public Item item;
    public int amount;

    public ItemListing(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}

public class Inventory : MonoBehaviour
{
    public List<ItemListing> inventory = new();
    public Dictionary<int, ItemListing> items = new();
    public int inventorySize = 20;
    public InventoryUI inventoryUI; // Reference to the InventoryUI component

    private void Start()
    {
        inventory.Clear();
    }

    public void populateInventory()
    {
        inventory.Clear();
        foreach (KeyValuePair<int, ItemListing> itemListing in items)
        {
            inventory.Add(new ItemListing(itemListing.Value.item, itemListing.Value.amount));
        }
    }

    public bool AddItem(Item item)
    {
        if (items.ContainsKey(item.ID))
        {
            items[item.ID].amount += 1;
        }
        else
        {
            if (items.Count < inventorySize)
            {
                items.Add(item.ID, new ItemListing(item, 1));
            }
            else
            {
                return false; // Inventory full
            }
        }
        populateInventory();
        NotifyInventoryChanged();
        return true;
    }

    public void RemoveItem(Item item)
    {
        if (items.ContainsKey(item.ID))
        {
            items.Remove(item.ID);
            populateInventory();
            NotifyInventoryChanged();
        }
    }
    
    public bool ReduceItem(Item item)
    {
        if (items.ContainsKey(item.ID))
        {
            items[item.ID].amount--;
            if (items[item.ID].amount <= 0)
            {
                items.Remove(item.ID);
            }
            populateInventory();
            NotifyInventoryChanged();
            return true;
        }
        return false;
    }

    public delegate void InventoryChanged();
    public event InventoryChanged OnInventoryChanged;

    private void NotifyInventoryChanged()
    {
        OnInventoryChanged?.Invoke();
        if (inventoryUI != null)
        {
            inventoryUI.RefreshUI();
        }
    }

    public ItemListing getItemListingByID(int id){
        if (items.ContainsKey(id))
        {
            return items[id];
        }else{
            Debug.Log("Null returning");
            return null;
        }

    }
}
