using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class enemyAI : MonoBehaviour
{
    public float distanceFromPlayer;
    public float attackCooldown = 2;
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
            direction = (targetTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Convert direction to angle in degrees
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            float speedup = 4; 
            transform.position += speed * speedup * Time.fixedDeltaTime * transform.up;
        }else if(aggro){
            direction = (targetTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Convert direction to angle in degrees
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Subtract 90 degrees to align the enemy's local Y-axis with the direction
            transform.position += speed * Time.fixedDeltaTime * transform.up;
        }else{
            direction = Vector3.zero;
        }
    }

    IEnumerator attack(float duration) {
    var time_start = Time.time;
    var time_end = time_start + duration;
    while(Time.time < time_end) {
        var t = (Time.time - time_start) / duration;
        transform.position += Vector3.Lerp(transform.position, transform.position+direction, t);
        yield return null;
    }}
}
