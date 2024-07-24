using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InsanityUI : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    public Image insanityUI;

    void Start()
    { 
        insanityUI = GetComponent<Image>();
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }
    public void Update(){
        if (insanityUI != null){
            insanityUI.fillAmount = playerController.insanity / 100f;
        }
    }
}
