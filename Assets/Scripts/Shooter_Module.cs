using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter_Module : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // A point from where the bullet will be fired
    public float fireRate = 1f; // Bullets per second
    
    

    private Coroutine shootingCoroutine;
    private Pathfinding_Physics pathfindingScript;

    
    void Start(){
        pathfindingScript = transform.GetComponent<Pathfinding_Physics>();
    }
    
    
    void Update()
    {
        if (pathfindingScript.isTargetInAttackRange)
        {
            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(Shoot());
            }
        }
        else
        {
            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
        }
    }

    

    IEnumerator Shoot()
    {
        while (true)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(1f / fireRate);
        }
    }
}
