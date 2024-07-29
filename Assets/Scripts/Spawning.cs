using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{   
    public GameObject raveger;
    public float spawnInterval = 5f;
    //GameObject player;
   // private bool spawning = false;
    public float distanceToTriggerSpawning = 15f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnRavager());
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator spawnRavager(){
        while(true){
            if(Vector3.Distance(transform.position,GameObject.FindGameObjectWithTag("Player").transform.position) < distanceToTriggerSpawning){
                Instantiate(raveger,transform.position+new Vector3((Random.value-0.5f)*10,(Random.value-0.5f)*10), Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
            }else{
                yield return new WaitForSeconds(1);
            }
        }
    }
}
