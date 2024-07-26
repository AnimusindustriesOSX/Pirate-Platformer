using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ConsumablesCanvas : MonoBehaviour
{
    public static int selected = 0;
    public static Image imageToSelect;
    public static GameObject parentObject;
    private static void Start() {
        parentObject = GameObject.FindGameObjectWithTag("ConsumablesCanvas");
    }
    public static void updateSelectedConsumable(int selectedItemSlot){
        if(imageToSelect!=null){
            imageToSelect.color = Color.white;
        }
        Debug.Log("" + selectedItemSlot);
        imageToSelect = GameObject.FindGameObjectWithTag("ConsumablesCanvas").transform.GetChild((selectedItemSlot-1)).GetComponent<Image>();
        imageToSelect.color = new Color(255,0,0,255);
    } 

    
}
