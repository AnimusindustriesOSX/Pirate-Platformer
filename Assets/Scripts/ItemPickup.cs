using UnityEditor;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // The item to be picked up
    public bool IsEnabled;

    private void Start() {
        IsEnabled = true;
    }

    public void ChangeEnable (bool enabled){
        IsEnabled = enabled;
    }

    public bool GetEnable(){
        return IsEnabled;
    }
}
