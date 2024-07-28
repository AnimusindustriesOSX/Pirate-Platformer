using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPatrol : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Transform target;
    [SerializeField] private Pathfinding_Physics pathfinding_Physics_script;
    
    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
