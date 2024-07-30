using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Regrow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer, spriteRendererShadow;
    [SerializeField] public List<Sprite> sprites;
    [SerializeField] public List<Sprite> shadowSprites;
    
    public int growthTimePerSprite = 10;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRendererShadow = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void Harvest(){
        //if its a seal plant call spawnUmbralOppressorOnHarvest()
        if(gameObject.GetComponent<ItemPickup>().item.ID == 4){
            if(gameObject.GetComponent<SealPlant>() != null){
                gameObject.GetComponent<SealPlant>().spawnUmbralOppressorOnHarvest();
            }
        }
        gameObject.GetComponent<ItemPickup>().ChangeEnable(false);
        StartCoroutine(HandleHarvest());
        
    }

    IEnumerator HandleHarvest(){
        spriteRendererShadow.color = Color.black;
        StartCoroutine(Grow(spriteRenderer,sprites));
        StartCoroutine(Grow(spriteRendererShadow,shadowSprites));
        
        gameObject.transform.GetChild(0).GetComponent<ItemPickup>().ChangeEnable(true);
        
        yield return new WaitForSeconds(0);
    }

    IEnumerator Grow(SpriteRenderer spriteRenderer, List<Sprite> sprites){
        spriteRenderer.sprite = sprites[0];
        int t=0;
        while(t < sprites.Count-1){
            t++;
            yield return new WaitForSeconds(growthTimePerSprite);
            spriteRenderer.sprite = sprites[t];
        }
        gameObject.GetComponent<ItemPickup>().ChangeEnable(true);
    }
}
