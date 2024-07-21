using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    //private Animator animator;
    public float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        StartCoroutine(AnimationCr(2f));
        //animator.SetTrigger("Attack");
        // Add more logic here, e.g., damage calculations, spawning projectiles, etc.
    }

    IEnumerator AnimationCr(float duration) {
    
    var time_start = Time.time;
    var time_end = time_start + duration;
    while(Time.time < time_end) {
        var t = (Time.time - time_start) / duration;
        transform.position = Vector3.Lerp(transform.position, transform.position+new Vector3(), t);
        yield return null;
    }
}
}