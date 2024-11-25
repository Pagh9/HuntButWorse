using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //wanna make shooting with ray casting. 
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootingRange = 100f;
    public LayerMask enemyLayer;
    public int bulletDamage = 150;
    public float fireRate = 0.5f;
    public float bulletSpeed = 50f;

    private float nextFireTime = 0f;


    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            
            nextFireTime = Time.time + fireRate;
            Shoot();
            
        }
    }

    void Shoot()
    {
        
        Debug.DrawRay(shootPoint.position, shootPoint.up * shootingRange, Color.red, 2f);
        RaycastHit hit;
        
        if (Physics.Raycast(shootPoint.position, Vector3.up, out hit, shootingRange, enemyLayer))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                Debug.Log($"Raycast hit: {hit.collider.name}");
                enemy.TakeDamage(bulletDamage);

            }
            else
            {
                Debug.LogError("Enemy script not found on hit object.");
            }
        }
        else
        {
            Debug.Log("Raycast missed.");
        }

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.damage = bulletDamage;

        }


        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.AddRelativeForce(Vector3.up * bulletSpeed);
            Debug.Log("Bullet velocity" + rb.velocity);
        }





    }
}
