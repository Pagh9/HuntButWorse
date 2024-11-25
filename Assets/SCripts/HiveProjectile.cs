using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveProjectile : MonoBehaviour
{
    
    public int rangedDamage = 10;
    public float speed = 10f;
    public float lifetime = 4f;
    public Transform player;

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("No GameObject with tag 'Player' found!");
            }
        }

        Destroy(gameObject, lifetime); // Automatically destroy after a set time
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate direction toward the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the projectile toward the player
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            Debug.LogWarning("Player reference is not assigned!");
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        
        
            
            
            if (other.gameObject.CompareTag("Player"))
            {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            
            playerHealth.TakeDamage(rangedDamage);
               Destroy(gameObject); // Destroy the projectile on hit
            }
        
  
        

        
    }
}
