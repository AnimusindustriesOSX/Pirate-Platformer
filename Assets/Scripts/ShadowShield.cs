using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowShield : MonoBehaviour
{   
    private float startTime;
    public int duration = 60;
    GameObject smallShadowDisc;
    void Start()
    {
        smallShadowDisc = GameObject.Find("Small_Player_Shadow_Disc");
        startTime = Time.time;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ShadowShieldUp = true;
        smallShadowDisc.GetComponent<ShadowDisc>().SpriteChange(1);
    }

    void Update()
    {
        if(Time.time - startTime > duration){
            smallShadowDisc.GetComponent<ShadowDisc>().SpriteChange(0);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ShadowShieldUp = false;
            Destroy(GetComponent<ShadowShield>());
        }
    }

    
}
