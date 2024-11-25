using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    
    public float speed = 5;
    private Transform player;
    public GameObject target;
    public int scoreValue = 50;
    public int moneyValue = 25;

    private Health enemyHealth;

    private Score scoreManager;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.Log("Player Object Not Found");
        }
        enemyHealth = GetComponent<Health>();
        if (enemyHealth == null)
        {
            
        }
    }

    private void Update()
    {

        MoveTowardsPlayer();
        

    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            transform.LookAt(new Vector3(player.position.x, transform.position.y, transform.position.z));

        }
    }

    public void TakeDamage(int damageAmount)
    {
       
        if (enemyHealth != null)
        {
            
            enemyHealth.TakeDamage(damageAmount);
            if (enemyHealth.currentHealth <= 0)
            {
                Die();
            }
        }
        else
        {
            Debug.LogError($"Enemy {gameObject.name} does not have a Health component assigned.");
        }



    }
    


    void Die()
    {
        Score.instance.AddScore(scoreValue);
        Score.instance.AddMoney(moneyValue);
        Destroy(target);
    }



}