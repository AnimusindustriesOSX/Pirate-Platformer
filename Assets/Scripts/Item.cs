using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name;
    public int ID;
    public bool isShadow;
    public Sprite Icon;
    virtual public void Effect(){

    }

    private void Start() {
        
    }

}
