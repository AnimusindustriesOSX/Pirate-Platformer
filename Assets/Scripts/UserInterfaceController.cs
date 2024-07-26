using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float health = 100;
    [SerializeField] int insanity;

    [SerializeField] int maxHealth = 100;
    [SerializeField] int maxInsanity = 100;

    [SerializeField] GameObject insanitySlider;
    [SerializeField] GameObject healthSlider;
    [SerializeField] GameObject insanityEye;
    PlayerController playerController;
    void Start(){
        insanity = 0;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        insanity = playerController.insanity;
        if(insanitySlider != null) {
            Image insanityBar = insanitySlider.GetComponent<Image>();
            //insanityBar.minValue = 0;
            //insanityBar.maxValue = maxInsanity;
            //insanityBar.value = insanity;

            if(insanityEye != null) {
                
                if(insanityBar.fillAmount == 1){
                    insanityEye.transform.GetComponent<Animator>().SetBool("isInsane",true);

                }
                else insanityEye.transform.GetComponent<Animator>().SetBool("isInsane",false);
            }    
        }
        

        if(healthSlider != null) {
            Slider healthBar = healthSlider.GetComponent<Slider>();
            healthBar.minValue= 0;
            healthBar.maxValue = 100;
            health=playerController.HP;
            healthBar.value = health;
        }

    }       
}
