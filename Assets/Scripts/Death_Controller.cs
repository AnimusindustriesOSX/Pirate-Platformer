using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPlayerDead;
    Transform player;
    PlayerController player_controller;
    Animator player_death_animator;
    private bool hasPlayed;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
        player_controller = player.GetComponent<PlayerController>();
        player_death_animator = player.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player_controller.HP <= 0)isPlayerDead = true;
        

        if(isPlayerDead == true){
            player_death_animator.SetBool("isDead", true);
            player_controller.speed = 0;

             AnimatorStateInfo stateInfo = player_death_animator.GetCurrentAnimatorStateInfo(0);

                if (stateInfo.IsName("Player_Death"))
                {
                    if (stateInfo.normalizedTime >= 1.0f && !hasPlayed)
                    {
                        hasPlayed = true;
                        //on animation end
                        transform.GetComponent<Animator>().SetBool("isDead", true);
                    }
                }
                else
                {
                    hasPlayed = false;
                }


        }

    }
}
