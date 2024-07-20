using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        if (freezeXZAxis){
            transform.rotation = Quaternion.Euler(0f,Camera.main.transform.rotation.eulerAngles.y,0); 
        }else{
            transform.rotation = Camera.main.transform.rotation;   
        }
    }
}
