using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Obelisk : MonoBehaviour
{
    public Enemy structure;
    public SpriteRenderer obeliskShadow;
    // Start is called before the first frame update
    void Start()
    {
        structure = GetComponent<Enemy>();
        obeliskShadow = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {   
        Color tmp = obeliskShadow.color;
        tmp.a = 1f-((float)(structure.maxHealth - structure.currentHealth)/(float)structure.maxHealth) ;
        obeliskShadow.color = tmp;
    }
}
