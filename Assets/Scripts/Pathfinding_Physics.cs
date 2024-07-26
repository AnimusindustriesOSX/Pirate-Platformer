using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Timers;

public class Pathfinding_Physics : MonoBehaviour
{
    
    
    [SerializeField] public float aggroTime;

    
    [Header("AI")]
    public bool isAIactive = true;
    public bool needDirectVision = true;
    public bool doesFollowWithoutSeekerModule = false;
    public Transform target; // Player target
    public Transform homePosition; // Home position for the character

    public float maxSpeed;
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
    public bool doesLookAtTarget;
    public Transform attackOrgan;
    private bool aiRotate = false;
    public float speedMultiplierDuringAttack;
    
    void Start()
    {
        seeker_Module = transform.GetComponent<Seeker_Module>();
        aiPath = GetComponent<AIPath>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiDestinationSetter.target = homePosition; // Start with home position
        idleTimer = idleTime;
        isIdle = true;
        wanderCenter = homePosition.position;
        aiRotate = aiPath.enableRotation;
    }

    void Update()
    {
        if (target == null)
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
        
        
        if(isAIactive && seeker_Module != null){
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);
            
                
                if(seeker_Module.isTargetDetected || (seeker_Module.isTargetInRange && doesFollowWithoutSeekerModule)){
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

                PerformAttack(distanceToPlayer);
                

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
    void PerformAttack(float distance){
        if(attackRange >= distance && seeker_Module.isTargetDetected){
                    attackOrgan.GetComponent<Animator>().SetBool("isAttacking", true);
                    aiPath.maxSpeed = maxSpeed * speedMultiplierDuringAttack;
                    if(doesLookAtTarget){
                        aiPath.enableRotation = false;
                        LookAtTarget();
                    }
                }
                else{
                    attackOrgan.GetComponent<Animator>().SetBool("isAttacking", false);
                    aiPath.maxSpeed = maxSpeed;
                    aiPath.enableRotation = aiRotate;
                }
    }

    void LookAtTarget(){
        if (target != null)
        {
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
        }
    }
}
