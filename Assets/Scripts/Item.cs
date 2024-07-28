using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name;
    public int ID;
    public bool isShadow;
    public Sprite Icon;
    GameObject player;
    private void Start() {
/*         player = GameObject.FindGameObjectWithTag("Player");
 */    }
    public void Effect(){
        switch (ID){
            case 21:
                GameObject.FindGameObjectWithTag("Player").AddComponent<DashPotion>();
                Debug.Log("dash script added");
                return;
            case 22:
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().healHP(30);
                Debug.Log("Healed 30 HP");
                return;
            case 23:
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().InsanityChangeSigned(-30);
                Debug.Log("Healed 30 insanity");
                return;
            case 24:
                GameObject.FindGameObjectWithTag("Player").AddComponent<LightPotion>();
                Debug.Log("light script added");
                return;
            case 31:
                GameObject.FindGameObjectWithTag("Player").AddComponent<ShadowShield>();
                Debug.Log("shadow shield script added");
                return;
            case 32:
                GameObject shadowGrenade = new GameObject("shadowGrenade");
                /* shadowGrenade.transform.SetParent(player.transform); */
                SpriteRenderer spriteRenderer = shadowGrenade.AddComponent<SpriteRenderer>();
                shadowGrenade.AddComponent<ShadowGrenade>();
                Debug.Log("done");
                return;
            default:
                return;
        }
    }
}
