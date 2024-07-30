using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

public class Prowler_Behavior : MonoBehaviour
{
     // The object to orbit around
    
    
    [Header("Orbit")]
    public float radius = 5f;       // The radius of the orbit
    public float speed = 5f;        // The speed of the orbit

    public float orbitTimer = 5f;
    
    [Header("Attack Dash")]
    public float attackDashDuration = 3.0f;
    public float dashSpeed = 30;
    public float visibilityTimeAfterDash = 2f;
    
    
    [Header("Fleeing")]
    public float fleeAngleRange = 90f;
    public float fleeRadius = 20f;
    public GameObject fleeLocationPrefab;
    [Header("Invisiblity")]
    
    public Transform invisiblityChild;
    
    
    
    
    
    
    
    
           // Current angle of the orbit
    private float tempOrbitTimer ;
    private float tempVisibilityTimeAfterDash;
    
    private Transform player;
    private Pathfinding_Physics pathfinder;
    private AIPath aIPath;
    private AIDestinationSetter aIDestinationSetter;
    
    [Header("Debug")]
    public bool isOrbiting = false;
    public bool isFleeing = false;

    public bool isAttacking = false;
    public GameObject currentFleeLocation;
    public float angle = 0f;
    private float elapsedTime = 0f;
    public float angleTowardsPlayer;

    private Vector3 dashStartposition;
    private Vector3 dashEndposition;
    private Vector3 dashDirection;

    void Start(){
        pathfinder= transform.GetComponent<Pathfinding_Physics>();
        aIPath = transform.GetComponent<AIPath>();
        aIDestinationSetter = transform.GetComponent<AIDestinationSetter>();
        
        radius = pathfinder.attackRange - 0.5f;
        tempOrbitTimer = orbitTimer;
        player = pathfinder.target;
       
    }
    
    
    void Update()
    {
        player = pathfinder.target;
        FindAngleTowardsPlayer();
        
        if(isOrbiting == false && isFleeing == false && isAttacking == false && pathfinder.isTargetInAttackRange){
            
            angle = angleTowardsPlayer;
            isOrbiting = true;
        }
        
        
        if (isOrbiting == true)
        {
            GoInvisible(true);
            player = pathfinder.target;
            aIPath.canMove = false;
            
            // Increase the angle based on time and speed
            
            // Calculate the new position
            float x = player.position.x + radius * Mathf.Cos(angle* Mathf.Deg2Rad);
            float y = player.position.y + radius * Mathf.Sin(angle* Mathf.Deg2Rad);
            angle += speed * Time.deltaTime;
            tempOrbitTimer -= Time.deltaTime;
             if (angle >= 360f)
            {
                angle -= 360f;
            }


            // Update the position of the orbiting object
            transform.position = new Vector3(x, y, transform.position.z);
        }
        if(tempOrbitTimer <= orbitTimer/4)GoInvisible(false);
        
        if(tempOrbitTimer<= 0){
            isAttacking = true;
            isOrbiting = false;
            tempOrbitTimer = orbitTimer;
            dashStartposition = transform.position;
            dashEndposition = player.position;
            dashDirection = (dashEndposition - dashStartposition).normalized;
        }
        
        
        
        if (isAttacking)
        {
            GoInvisible(false);
            elapsedTime += Time.deltaTime;

            // Calculate the interpolation factor using an ease-out function
            float t = elapsedTime / attackDashDuration;
            float easeOutT = EaseOutCubic(t);

            // Move the object towards the target position
             transform.position += dashDirection * dashSpeed * easeOutT * Time.deltaTime;

            // Stop moving after the duration is reached
            if (elapsedTime >= attackDashDuration)
            {
                //transform.position = dashEndposition;
                isAttacking = false;
                isFleeing = true;
                elapsedTime = 0f; 
                
                tempVisibilityTimeAfterDash = visibilityTimeAfterDash;
                pathfinder.target = FindLocationToFlee();
            }

            
        
        }



        if(isFleeing){
            if(tempVisibilityTimeAfterDash > 0){
                tempVisibilityTimeAfterDash -= Time.deltaTime;
            }
            else{
                GoInvisible(true);
                tempVisibilityTimeAfterDash = 0;
            }
            aIPath.canMove = true;
            pathfinder.disableAttack = true;
            if (currentFleeLocation != null)
            {
                // Check the distance between this object and the target prefab
                float distance = Vector3.Distance(transform.position,currentFleeLocation.transform.position);

                // If within destruction distance, destroy the prefab instance
                Debug.Log(distance);
                if (distance <= 5)
                {
                    pathfinder.target = player;
                    isFleeing = false;
                    pathfinder.disableAttack = false;
                    Destroy(currentFleeLocation);
                    Debug.Log("Target prefab destroyed.");
                    
                }
            }
        }

        
    }
    float EaseOutCubic(float t)
    {
        t = Mathf.Clamp01(t);
        return 1f - Mathf.Pow(1f - t, 3f);
    }
    void FindAngleTowardsPlayer(){
        
        Vector2 direction = player.position - transform.position;
        angleTowardsPlayer = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        
        
    }
    Transform FindLocationToFlee(){
        float angle = angleTowardsPlayer;
        angle = Mathf.PI - angle;
        angle = UnityEngine.Random.Range(angle-fleeAngleRange, angle+fleeAngleRange); // Random angle in radians
        float x = fleeRadius * Mathf.Cos(angle);
        float y = fleeRadius * Mathf.Sin(angle);
        Vector3 spawnPosition = new Vector3(x, y,transform.position.z); // Example position, set as needed
        currentFleeLocation = Instantiate(fleeLocationPrefab, spawnPosition, Quaternion.identity);
        
        
        return currentFleeLocation.transform;
    }
    void GoInvisible(bool isInvis){
        if (isInvis){   
            invisiblityChild.GetComponent<Animator>().SetBool("isInvisible",true);
        }  
        else{
            invisiblityChild.GetComponent<Animator>().SetBool("isInvisible",false);
        }
    }
}
