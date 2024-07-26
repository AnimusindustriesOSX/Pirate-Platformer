using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI[] itemTextDisplays = new TextMeshProUGUI[14];
    public Inventory inventory;
    private Dictionary<int, int> itemIdToDisplayIndex = new Dictionary<int, int>
    {
        { 1, 0 },
        { 2, 1 },
        { 3, 2 },
        { 4, 3 },
        { 11, 4 },
        { 12, 5 },
        { 13, 6 },
        { 21, 7 },
        { 22, 8 },
        { 23, 9 },
        { 24, 10 },
        { 31, 11 },
        { 32, 12 },
    };

    private void Start()
    {   
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if (itemTextDisplays.Length == 0)
        {
            Debug.LogError("No TextMeshProUGUI elements assigned to itemTextDisplays.");
            return;
        }

        // Register to the Inventory's event
        if (inventory != null)
        {
            inventory.OnInventoryChanged += UpdateTextDisplays;
            UpdateTextDisplays(); // Initial update
        }
        else
        {
            Debug.LogError("Inventory singleton instance is null.");
        }
    }

    private void OnDestroy()
    {
        if (inventory != null)
        {
            inventory.OnInventoryChanged -= UpdateTextDisplays;
        }
    }

    private void UpdateTextDisplays()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory instance is null during UpdateTextDisplays.");
            return;
        }

        for (int i = 0; i < itemTextDisplays.Length; i++)
        {
            if (itemTextDisplays[i] != null)
            {
                itemTextDisplays[i].text = "0";
            }
            else
            {
                Debug.LogError("TextMeshProUGUI element at index " + i + " is null.");
            }
        }

        foreach (var itemListing in inventory.items)
        {
            if (itemIdToDisplayIndex.TryGetValue(itemListing.Key, out int displayIndex))
            {
                if (displayIndex >= 0 && displayIndex < itemTextDisplays.Length)
                {
                    if (itemTextDisplays[displayIndex] != null)
                    {
                        itemTextDisplays[displayIndex].text = itemListing.Value.amount.ToString();
                    }
                    else
                    {
                        Debug.LogError("TextMeshProUGUI element at displayIndex " + displayIndex + " is null.");
                    }
                }
                else
                {
                    Debug.LogError("DisplayIndex " + displayIndex + " is out of range.");
                }
            }
            else
            {
                Debug.LogError("Item ID " + itemListing.Key + " not mapped to display.");
            }
        }
    }

    public void RefreshUI()
    {
        UpdateTextDisplays();
    }
}
