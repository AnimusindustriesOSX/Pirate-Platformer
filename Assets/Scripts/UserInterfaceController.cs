using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int health = 100;
    [SerializeField] int insanity = 100;

    [SerializeField] int maxHealth = 100;
    [SerializeField] int maxInsanity = 100;

    [SerializeField] GameObject insanitySlider;
    [SerializeField] GameObject healthSlider;
    [SerializeField] GameObject insanityEye;
    

    // Update is called once per frame
    void Update()
    {
        if(insanitySlider != null) {
            Slider insanityBar = insanitySlider.GetComponent<Slider>();
            insanityBar.minValue = 0;
            insanityBar.maxValue = maxInsanity;
            insanityBar.value = insanity;

            if(insanityEye != null) {
                if(insanityBar.maxValue <= insanityBar.value){
                    insanityEye.transform.GetComponent<Animator>().SetBool("isInsane",true);

                }
                else insanityEye.transform.GetComponent<Animator>().SetBool("isInsane",false);
            }    
        }
        

        if(healthSlider != null) {
            Slider healthBar = healthSlider.GetComponent<Slider>();
            healthBar.minValue= 0;
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }

    }       
}
