using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter_Module : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // A point from where the bullet will be fired
    public float fireRate = 1f; // Bullets per second
    
    

    private Coroutine shootingCoroutine;
    private Seeker_Module seeker_Module;

    
    void Start(){
        seeker_Module = transform.GetComponent<Seeker_Module>();
    }
    
    
    void Update()
    {
        if (seeker_Module.isTargetDetected)
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
