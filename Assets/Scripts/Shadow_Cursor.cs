using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_Cursor : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] Transform Cursor;
    [SerializeField] Transform shadow_Gun;
    private GameObject player;
    public Vector3 offset;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update(){
        //follow player
        if(player != null) transform.position = player.transform.position + offset;

        //gun look at shadow cursor
        
    
    
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    // create a plane at 0,0,0 whose normal points to +Z:
    Plane hPlane = new Plane(Vector3.forward, Vector3.zero);
    // Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
    float distance = 0; 
    // if the ray hits the plane...
    if (hPlane.Raycast(ray, out distance)){
    // get the hit point:
        Cursor.position = ray.GetPoint(distance);
        }
    GunLookAtCursor();
}

void GunLookAtCursor(){

    Vector2 direction = Cursor.position - shadow_Gun.position;
            
    // Calculate the angle between the object's current direction and the target
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // Apply the rotation
    shadow_Gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
}
}
