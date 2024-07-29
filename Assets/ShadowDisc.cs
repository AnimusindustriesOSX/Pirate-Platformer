using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowDisc : MonoBehaviour
{   
    public Sprite [] sprites;
    public SpriteRenderer  spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer =  GetComponent<SpriteRenderer>();
    }

    public void SpriteChange(int index){
        if(index== 1){
            transform.localScale *= 5;
        }else{
            transform.localScale /= 5;
        }
        spriteRenderer.sprite = sprites[index];
    }
}
