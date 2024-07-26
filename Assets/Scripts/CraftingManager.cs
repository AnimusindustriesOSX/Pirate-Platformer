using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private CraftingItem currentItem; // The item currently being interacted with
    public Image customCursor; // The image for the custom cursor to show the item being dragged
    public CraftingSlot[] craftingSlots; // Slots available for crafting items
    public List<CraftingItem> itemList; // Current items in crafting slots
    public string[] recipes; // Recipe identifiers as strings
    public CraftingItem[] recipeResults; // The results of crafting specific recipes
    public CraftingSlot resultSlot; // Slot to display crafted result
    public Inventory playerInventory; // The player's inventory

    public GameObject CraftingCanvas;
    private void Start()
    {
        // Find the player GameObject by tag and get its Inventory component
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerInventory = player.GetComponent<Inventory>();
            if (playerInventory == null)
            {
                Debug.LogError("No Inventory component found on the Player GameObject.");
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Player' found.");
        }
    }

    private void Update()
    {
        // Handle mouse release to place item into crafting slot
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                customCursor.gameObject.SetActive(false);
                CraftingSlot nearestSlot = FindNearestSlot();

                if (nearestSlot != null)
                {
                    if (CanPlaceItem(currentItem))
                    {
                        PlaceItemInSlot(nearestSlot);
                    }
                    else
                    {
                        Debug.Log($"Not enough {currentItem.itemName} in inventory.");
                    }
                }

                currentItem = null;
                CheckForCreatedRecipes();
            }
        }
    }

    private CraftingSlot FindNearestSlot()
    {
        CraftingSlot nearestSlot = null;
        float shortestDistance = float.MaxValue;

        foreach (CraftingSlot slot in craftingSlots)
        {
            float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                nearestSlot = slot;
            }
        }

        return nearestSlot;
    }

    private void PlaceItemInSlot(CraftingSlot slot)
    {
        slot.gameObject.SetActive(true);
        slot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
        slot.item = currentItem;
        itemList[slot.index] = currentItem;

        // Reduce item count in inventory immediately after placing
        playerInventory.ReduceItem(currentItem.item);
        Debug.Log($"Placed {currentItem.itemName} into crafting slot and reduced from inventory.");
    }

    private bool CanPlaceItem(CraftingItem craftingItem)
    {
        ItemListing itemListing;
        return playerInventory.items.TryGetValue(craftingItem.ID, out itemListing) && itemListing.amount > 0;
    }

    private void CheckForCreatedRecipes()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = CreateRecipeString();

        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                ShowCraftingResult(i);
                break; // Stop checking once a match is found
            }
        }
    }

    private string CreateRecipeString()
    {
        string recipeString = "";
        foreach (CraftingItem item in itemList)
        {
            if (item != null)
            {
                recipeString += item.itemName;
            }
            else
            {
                recipeString += "null";
            }
        }
        return recipeString;
    }

    private void ShowCraftingResult(int recipeIndex)
    {
        resultSlot.gameObject.SetActive(true);
        resultSlot.GetComponent<Image>().sprite = recipeResults[recipeIndex].GetComponent<Image>().sprite;
        resultSlot.item = recipeResults[recipeIndex];

        Debug.Log(recipeResults[recipeIndex].itemName + " crafted.");
    }

    private void ClearCraftingSlots()
    {
        foreach (CraftingSlot slot in craftingSlots)
        {
            if (slot.item != null)
            {
                slot.item = null;
                slot.GetComponent<Image>().sprite = null; // Clear the sprite
                slot.gameObject.SetActive(false);
                itemList[slot.index] = null;
            }
        }
    }

    public void OnClickSlot(CraftingSlot slot)
    {
        if (slot.item != null)
        {
            playerInventory.AddItem(slot.item.item); // Return item to inventory
            itemList[slot.index] = null;
            slot.item = null;
            slot.gameObject.SetActive(false);
            CheckForCreatedRecipes();
        }
    }

    public void OnMouseDownItem(CraftingItem item)
    {
        if (currentItem == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
    }

    public void OnClickResultSlot()
    {
        if (resultSlot.item != null)
        {
            // Add the crafted item to the inventory
            playerInventory.AddItem(resultSlot.item.item);

            // Log the successful addition
            Debug.Log("Added crafted item to inventory: " + resultSlot.item.item.Name + resultSlot.item.item.ID);

            CraftingCanvas.SetActive(false);
            Time.timeScale = 1;

            // Clear the crafting slots and reset them
            ClearCraftingSlots();

            // Hide the result slot after adding to inventory
            resultSlot.gameObject.SetActive(false);
            resultSlot.item = null;
        }
    }
}
