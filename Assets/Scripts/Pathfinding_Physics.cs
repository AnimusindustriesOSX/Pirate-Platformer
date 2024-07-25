using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Timers;

public class Pathfinding_Physics : MonoBehaviour
{
    
    
    [SerializeField] public float aggroTime;

    
    
    public bool isAIactive = true;
    public Transform target; // Player target
    public Transform homePosition; // Home position for the character
    public float detectionRadius = 5f; // Radius to detect the player
    public float idleTime = 2f; // Time to spend idling
    public float wanderRadius = 10f; // Radius within which the character can wander

    private Seeker_Module seeker_Module;
    private AIPath aiPath;
    private AIDestinationSetter aiDestinationSetter;
    private float idleTimer;
    private bool isIdle;
    private Vector2 wanderCenter;
    
    private float secAggro;
    
    [Header("Attack")]
    public float attackRange;
    public Transform attackOrgan;
    
    void Start()
    {
        seeker_Module = transform.GetComponent<Seeker_Module>();
        aiPath = GetComponent<AIPath>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiDestinationSetter.target = homePosition; // Start with home position
        idleTimer = idleTime;
        isIdle = true;
        wanderCenter = homePosition.position;
    }

    void Update()
    {
        if (target == null)
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
        
        
        if(isAIactive && seeker_Module){
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);
            
                
                if(seeker_Module.isTargetDetected ){
                    secAggro = aggroTime;
                }
                
                if( secAggro > 0){
                    secAggro -= Time.deltaTime;
                    aiDestinationSetter.target = target;
                    isIdle = false;
                }
                else{
                    aiDestinationSetter.target = homePosition;
                    isIdle = true;
                    secAggro = 0;
                }


                if(attackRange >= distanceToPlayer){
                    attackOrgan.GetComponent<Animator>().SetBool("isAttacking", true);
                }
                else{
                    attackOrgan.GetComponent<Animator>().SetBool("isAttacking", false);
                }

                }
                /**
                if (distanceToPlayer <= detectionRadius)
                {
                    // Player detected, follow the player
                    aiDestinationSetter.target = target;
                    isIdle = false;
                }
                else if (distanceToPlayer > detectionRadius && !isIdle)
                {
                    // Player lost, return to home position
                    aiDestinationSetter.target = homePosition;
                    isIdle = true;
                }
                **/
            
            // Handle idling
            if (isIdle)
            {
                idleTimer -= Time.deltaTime;
                if (idleTimer <= 0)
                {
                    // Perform idle actions (e.g., animations) and wander randomly
                    Wander();
                    idleTimer = idleTime; // Reset idle timer
                }
            }
        }
    

    void Wander()
    {
        Vector2 randomDirection = Random.insideUnitCircle * wanderRadius;
        Vector2 wanderTarget = wanderCenter + randomDirection;
        aiDestinationSetter.target.position = wanderTarget;
    }
}
