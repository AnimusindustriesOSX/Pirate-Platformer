using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShadeRaveger : MonoBehaviour
{   
    Animator animator;
    ShadeRaveger prefab;
    Enemy shade;
    public float avoidanceRadius = 0.5f;
    void Start(){
        prefab = PrefabUtility.GetCorrespondingObjectFromSource(this);
        shade = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }
    void Update(){
        transform.position = new Vector3(transform.position.x, transform.position.y,0);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            shade.TakeDamage(shade.maxHealth);
        }else if (PrefabUtility.GetCorrespondingObjectFromSource(other) == prefab)
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            transform.position -= direction * avoidanceRadius; // Adjust position to prevent overlap
        }
    }
}
