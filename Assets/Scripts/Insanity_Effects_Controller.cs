using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insanity_Effects_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool isPlayerInsane;
    [SerializeField] Transform Player_Head;
    private Animator head_Animator;
    private PlayerController player;
    
    void Start()
    {
        head_Animator = Player_Head.GetComponent<Animator>();
        player = transform.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.insanity >= player.MaxInsanity){
            isPlayerInsane = true;
        }
        else isPlayerInsane = false;

        InsanityEyes();

    }
    void InsanityEyes(){
        if(isPlayerInsane == true){
            head_Animator.SetBool("isInsane",true);
        }
        else{
            head_Animator.SetBool("isInsane",false);
        }
    }
}
