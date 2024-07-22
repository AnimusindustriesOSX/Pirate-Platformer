using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class layerControl : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y*100);
    }
}
