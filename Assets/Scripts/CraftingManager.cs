using System.Collections;
using System.Collections.Generic; // Required for List<T>
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private CraftingItem currentItem;
    public Image customCursor;
    public CraftingSlot[] craftingSlots;
    public List<CraftingItem> itemList; // Ensure List<T> is recognized
    public string[] recipes;
    public CraftingItem[] recipeResults;
    public CraftingSlot resultSlot;

    public Inventory playerInventory; // Reference to the player's inventory

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
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                customCursor.gameObject.SetActive(false);
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

                if (nearestSlot != null)
                {
                    if (CanPlaceItem(currentItem))
                    {
                        nearestSlot.gameObject.SetActive(true);
                        nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                        nearestSlot.item = currentItem;
                        itemList[nearestSlot.index] = currentItem;
                        playerInventory.ReduceItem(currentItem.item); // Update inventory count
                    }
                    else
                    {
                        Debug.Log("Not enough " + currentItem.itemName + " in inventory.");
                    }
                }

                currentItem = null;
                CheckForCreatedRecipes();
            }
        }
    }

    private bool CanPlaceItem(CraftingItem craftingItem)
    {
        ItemListing itemListing;
        return playerInventory.items.TryGetValue(craftingItem.ID, out itemListing) && itemListing.ammount > 0;
    }

    private void CheckForCreatedRecipes()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = "";
        foreach (CraftingItem item in itemList)
        {
            if (item != null)
            {
                currentRecipeString += item.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }

        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResults[i];
                DeductItemsForRecipe(currentRecipeString);

                StartCoroutine(FlashAndAddToInventory(resultSlot.item));
            }
        }
    }

    private IEnumerator FlashAndAddToInventory(CraftingItem craftedItem)
    {
        Image resultImage = resultSlot.GetComponent<Image>();
        for (int i = 0; i < 6; i++) // Flash 3 times
        {
            resultImage.enabled = !resultImage.enabled;
            yield return new WaitForSeconds(0.25f);
        }

        if (craftedItem != null)
        {
            playerInventory.AddItem(craftedItem.item);
        }

        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;
    }

    private void DeductItemsForRecipe(string currentRecipeString)
    {
        foreach (CraftingItem item in itemList)
        {
            if (item != null)
            {
                playerInventory.ReduceItem(item.item);
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
}
