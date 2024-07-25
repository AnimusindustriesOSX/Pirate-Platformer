using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ItemListing{ 
    public ItemListing(Item item, int ammount) {
        this.item = item;
        this.ammount = ammount;
    }
    [SerializeField]public Item item;
    [SerializeField]public int ammount;
}
public class Inventory : MonoBehaviour
{
    public List<ItemListing> inventory = new();
    public Dictionary<int,ItemListing> items = new();
    public int inventorySize = 20;
    private void Start() {
        inventory.Clear();
    }

    public void populateInventory(){
        inventory.Clear();
        foreach(KeyValuePair<int,ItemListing> itemListing in items){
            inventory.Add(new ItemListing(itemListing.Value.item,itemListing.Value.ammount));
            Debug.Log("item added");
        }
        
    }

    public bool AddItem(Item item)
    {
        if(items.ContainsKey(item.ID)){
            if(items[item.ID].ammount >=1){
                items[item.ID].ammount +=1;
            }else{
                items[item.ID].ammount =1;
            }
        }else{
            if (items.Count < inventorySize){
                items.Add(item.ID,new ItemListing(item,1));
            }else{
                return false;
            }
        }   
        populateInventory();
        return true;         
    }


    public void RemoveItem(Item item){
        if (items.ContainsKey(item.ID)){
            items.Remove(item.ID);
            Debug.Log("Removed: " + item.Name);
            populateInventory();
        }else{
            Debug.Log("Tried to remove item:"  + item.Name + "but it wasn't found: ");
            populateInventory();
        }
    }

    public bool ReduceItem(Item item){
        if (items.ContainsKey(item.ID)){
            items[item.ID].ammount --;
            Debug.Log("Removed 1 instance of: " + item.Name);
            return true;
        }else{
            Debug.Log("Tried to remove item:"  + item.Name + "but it wasn't found: ");
            return false;
        }
    }
}
