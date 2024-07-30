using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SetLayerAsParent : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] int offset = 0;
    [SerializeField] Transform highestParent;

    private int parent_layer;
   

    // Update is called once per frame
    void Update()
    {
        parent_layer = highestParent.GetComponent<SpriteRenderer>().sortingOrder ;
        transform.GetComponent<SpriteRenderer>().sortingOrder = parent_layer + offset;
    }
}
