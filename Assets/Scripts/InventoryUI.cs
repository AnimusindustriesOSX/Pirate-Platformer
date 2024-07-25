using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : Inventory
{
    // Array to store references to TextMeshProUGUI elements for displaying item counts
    public TextMeshProUGUI[] itemTextDisplays = new TextMeshProUGUI[13];

    // Mapping from item IDs to indices in the text display array
    private Dictionary<int, int> itemIdToDisplayIndex = new Dictionary<int, int>
    {
        { 0, 0 },  // Example mapping: Item ID 0 corresponds to the first text display
        { 1, 1 },  // Example mapping: Item ID 1 corresponds to the second text display
        { 2, 2 },  // Continue mapping for each item ID...
        { 3, 3 },
        { 4, 4 },
        { 5, 5 },
        { 6, 6 },
        { 7, 7 },
        { 8, 8 },
        { 9, 9 },
        { 10, 10 },
        { 11, 11 },
        { 12, 12 },
    };

    // Updates the text displays based on the current inventory state
    private void UpdateTextDisplays()
    {
        // Clear all displays initially
        for (int i = 0; i < itemTextDisplays.Length; i++)
        {
            itemTextDisplays[i].text = "0";
        }

        // Populate displays based on current inventory
        foreach (var itemListing in items)
        {
            if (itemIdToDisplayIndex.TryGetValue(itemListing.Key, out int displayIndex))
            {
                itemTextDisplays[displayIndex].text = itemListing.Value.ammount.ToString();
            }
        }
    }

    // Override the AddItem method to update the UI when an item is added
    public new bool AddItem(Item item)
    {
        // Call base method to add the item
        bool added = base.AddItem(item);

        // Update UI if the item was successfully added
        if (added)
        {
            UpdateTextDisplays();
        }
        return added;
    }

    // Override the RemoveItem method to update the UI when an item is removed
    public new void RemoveItem(Item item)
    {
        // Call base method to remove the item
        base.RemoveItem(item);

        // Update UI after removing the item
        UpdateTextDisplays();
    }

    // Override the ReduceItem method to update the UI when an item count is reduced
    public new void ReduceItem(Item item)
    {
        // Call base method to reduce item count
        base.ReduceItem(item);

        // Update UI after reducing the item
        UpdateTextDisplays();
    }
}
