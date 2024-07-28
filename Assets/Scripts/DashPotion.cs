using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPotion : MonoBehaviour
{
    PlayerController playerController;
    Rigidbody2D rb;
    private float startTime;
    public int duration = 10;
    public float dashDistance = 10;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        Debug.Log("DashPotion");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime > duration){
            Destroy(GetComponent<DashPotion>());
        }
    }

    private void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            rb.MovePosition(rb.position + playerController.direction * dashDistance);
        }
    }
}
