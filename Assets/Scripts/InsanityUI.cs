using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class InsanityUI : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    public Image insanityUI;
    public float Insanity;//0-1
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
            Insanity = playerController.insanity / 100f;
            insanityUI.fillAmount = Insanity;
        }
    }
}
