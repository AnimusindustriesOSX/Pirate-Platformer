using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{   
    public GameObject raveger;
    public float spawnInterval = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnRavager());
    }

    IEnumerator spawnRavager(){
        while(true){
            Instantiate(raveger,transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
