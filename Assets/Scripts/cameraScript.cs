using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] Vector3 distanceFromPlayer = new Vector3(0,-15,-15);
    //[SerializeField] Vector3 rotation = new Vector3(0,0,0);
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.position = player.transform.position + distanceFromPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + distanceFromPlayer;
    }
}
