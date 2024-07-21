using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regrow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer, spriteRendererShadow;
    [SerializeField] public List<Sprite> sprites;
    [SerializeField] public List<Sprite> shadowSprites;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRendererShadow = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void Harvest(){
        spriteRendererShadow.color = Color.black;
        StartCoroutine(Grow(spriteRenderer,sprites));
        StartCoroutine(Grow(spriteRendererShadow,shadowSprites));
        
    }

    IEnumerator Grow(SpriteRenderer spriteRenderer, List<Sprite> sprites){
        spriteRenderer.sprite = sprites[0];
        Debug.Log("!");
        int t=0;
        while(t < sprites.Count-1){
            t++;
            yield return new WaitForSeconds(5);
            spriteRenderer.sprite = sprites[t];
        }
    }
}
