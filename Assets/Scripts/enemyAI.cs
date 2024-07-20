using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class enemyAI : MonoBehaviour
{
    public float distanceFromPlayer;
    public float speed = 2;
    public float aggroCombatRange = 3;
    public float aggroRange = 10;
    public float aggroMaxRange = 15;
    public GameObject target;
    public Transform targetTransform;
    private Vector3 direction; 
    private bool aggro;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("body");
        targetTransform = target.transform;
        aggro = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        targetTransform = target.transform;
        distanceFromPlayer = (targetTransform.position - transform.position).magnitude;
        if(distanceFromPlayer <= aggroRange){
            aggro = true;
        }else if( distanceFromPlayer >= aggroMaxRange){
            aggro = false;
        }

        if( aggro && distanceFromPlayer<=aggroCombatRange){
            //enemy attack
        }
        if(aggro){
            direction = (targetTransform.position - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;
        }else{
            direction = Vector3.zero;
        }
    }

}
