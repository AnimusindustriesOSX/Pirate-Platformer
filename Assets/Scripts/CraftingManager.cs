using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    // Variable to store the currently selected crafting item
    private CraftingItem currentItem;

    // Reference to the UI Image component used as a custom cursor
    public Image customCursor;

    // Array of crafting slots available in the UI
    public CraftingSlot[] craftingSlots;

    // List to track items in the crafting slots
    public List<CraftingItem> itemList;

    // Array of recipe strings (e.g., "itemAitemB")
    public string[] recipes;

    // Array of crafting results corresponding to recipes
    public CraftingItem[] recipeResults;

    // Reference to the result slot where crafted items appear
    public CraftingSlot resultSlot;

    // Update is called once per frame
    private void Update()
    {
        // Check if the left mouse button was released
        if (Input.GetMouseButtonUp(0))
        {
            // Check if there is a currently selected item
            if (currentItem != null)
            {
                // Hide the custom cursor when the item is dropped
                customCursor.gameObject.SetActive(false);

                // Variables to track the nearest crafting slot
                CraftingSlot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                // Iterate over all crafting slots to find the nearest one
                foreach (CraftingSlot slot in craftingSlots)
                {
                    // Calculate the distance between the mouse position and the slot position
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);

                    // Update the nearest slot if the current slot is closer
                    if (dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }

                // After finding the nearest slot, place the item in it
                if (nearestSlot != null)
                {
                    nearestSlot.gameObject.SetActive(true);
                    nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                    nearestSlot.item = currentItem;
                    itemList[nearestSlot.index] = currentItem;  // Update the item list with the current item
                }

                // Clear the current item after placing it
                currentItem = null;

                // Check for any recipes that can be created with the current items
                CheckForCreatedRecipes();
            }
        }
    }

    // Check if the items in slots match any recipe and produce the result if so
    void CheckForCreatedRecipes()
    {
        // Hide the result slot initially and clear its item
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        // Build the current recipe string from items in the crafting slots
        string currentRecipeString = "";
        foreach (CraftingItem item in itemList)
        {
            if (item != null)
            {
                currentRecipeString += item.itemName;  // Concatenate item names for recipe matching
            }
            else
            {
                currentRecipeString += "null";  // Mark empty slots with "null"
            }
        }

        // Check against all recipes to find a match
        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                // If a match is found, display the result in the result slot
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResults[i];
            }
        }
    }

    // Method to clear a slot when it is clicked
    public void OnClickSlot(CraftingSlot slot)
    {
        slot.item = null;  // Clear the item in the slot
        itemList[slot.index] = null;  // Update the itemList to reflect the slot is now empty
        slot.gameObject.SetActive(false);  // Hide the slot

        // Recheck recipes after clearing a slot
        CheckForCreatedRecipes();
    }

    // Method called when an item is clicked on
    public void OnMouseDownItem(CraftingItem item) // Allows the picking up of an item and making the cursor the item
    {
        // Check if there is no item currently selected
        if (currentItem == null)
        {
            // Set the clicked item as the current item
            currentItem = item;

            // Enable the custom cursor and set its sprite to the current item's sprite
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
    }
}
