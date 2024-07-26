using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name;
    public int ID;
    public bool isShadow;
    public Sprite Icon;
    public void Effect(){
        switch (ID){
            case 21:
                return;
            case 22:
                return;
            case 23:
                return;
            case 24:
                return;
            case 31:
                return;
            case 32:
                return;
        }
    }

    private void Start() {
        
    }

}
