using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Vector2 _input;
    private Vector3 direction;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (direction != Vector3.zero)
        {

            gameObject.transform.position += speed * Time.deltaTime * direction;
            if(direction.x < 0) {
                transform.localScale = new Vector3(1,1,1);
            }
            else if(direction.x > 0){
                transform.localScale = new Vector3(-1,1,1);
            } 
        }
    }

    
}
