using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Seeker_Module : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] public Transform target;
    [SerializeField] public float aggroRangeX;
    [SerializeField] public float aggroRangeY;

    [SerializeField] public LayerMask obstacleLayer;

    public bool isTargetDetected;
    public bool isTargetInRange;


    [SerializeField] float distanceToPlayerX;
    [SerializeField] float distanceToPlayerY;

    private bool isObstacleInWay;
    private Vector2 start;
    private Vector2 end;
    private float distanceOffSet;

    
    public bool doesLookAtTarget = true;
    public Transform childToRotate;
    
    [Header("Debug")]
    [SerializeField]  public bool isTargetOnRight;
    
    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    
    void Update()
    {
        
        if(doesLookAtTarget && childToRotate != null){

            if(isTargetOnRight)childToRotate.localScale = new Vector3(-1,1,1);
            else childToRotate.localScale = new Vector3(1,1,1);
        }
        
        
        
        
        
        if (target == null)
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        //distanceToPlayerX = Vector2.Distance(transform.position, target.position);
        if(transform.position.x - target.position.x > 0)isTargetOnRight = false;
        else isTargetOnRight = true;
        distanceToPlayerX = math.abs(transform.position.x - target.position.x);
        distanceToPlayerY = math.abs(transform.position.y - target.position.y);
        if (distanceToPlayerX <= aggroRangeX && distanceToPlayerY <= aggroRangeY)
            {
                isTargetInRange = true;
                Color rayColor;
                
                
                
                start = transform.position;
                end = target.transform.position;
                distanceOffSet= (target.GetComponent<BoxCollider2D>().size.y)/2 - 0.1f;
                Vector2 offsetDirection1 = new Vector2(end.x,end.y + distanceOffSet);
                Vector2 offsetDirection2 = new Vector2(end.x,end.y - distanceOffSet);
                Vector2 direction = (end- start).normalized;
                Vector2 direction2 = ( offsetDirection1 - start).normalized;
                Vector2 direction3 = ( offsetDirection2 - start).normalized;
                float distance = Vector2.Distance(start, end);
                float distance2 = Vector2.Distance(start, offsetDirection1);
                float distance3 = Vector2.Distance(start, offsetDirection2);
                // target in range, check if no colliders are in line of sight
                isObstacleInWay = Physics2D.Raycast(start, direction, distance, obstacleLayer) && Physics2D.Raycast(start, direction2, distance2, obstacleLayer) && Physics2D.Raycast(start, direction3, distance3, obstacleLayer);

                if (isObstacleInWay){
                    rayColor = Color.red;
                    isTargetDetected = false;
                }
                else{
                    rayColor = Color.green;
                    isTargetDetected = true;
                }
                //draw rays
                Debug.DrawRay(start, direction * distance, rayColor);
                Debug.DrawRay(start, direction2 * distance2, rayColor);
                Debug.DrawRay(start, direction3 * distance3, rayColor);
            
            }
        else{
            isTargetDetected = false;
            isTargetInRange = false;
        }
    }
}
