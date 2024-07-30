using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealPlant : MonoBehaviour
{
    public GameObject umbralOppressor;
    public void spawnUmbralOppressorOnHarvest(){
        Instantiate(umbralOppressor, gameObject.transform.position, Quaternion.identity);
    }
}
