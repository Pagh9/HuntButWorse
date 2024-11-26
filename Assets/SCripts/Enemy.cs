using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    
    public float speed = 5;
    private Transform player;
    public GameObject target;
    public int scoreValue = 50;
    public int moneyValue = 25;

    private Health enemyHealth;
    private Score scoreManager;

    public bool canMeleeAttack = true;
    public bool canRangedAttack = true;
    public float meleeRange = 2f;
    public int meleeDamage = 25;
    public float rangedRange = 10f;
    public int rangedDamage = 10;
    public float attackCooldown = 2f;
    public GameObject hiveAttackPrefab;
    public Transform hiveAttackSpawnPoint;
    public float hiveBulletSpeed = 5.5f;

    private float lastAttackTime;
    public NavMeshAgent nav;
    

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
            Debug.LogWarning("Enemy health component not assigned.");
        }
        nav = GetComponent<NavMeshAgent>();
        if (nav == null)
        {
            Debug.LogError("NavMeshAgent component not found on Enemy.");
        }
        nav.speed = speed; // Set speed
        nav.stoppingDistance = meleeRange; // Set stopping distance
    }

    private void Update()
    {

        MoveTowardsPlayer();
        TryAttack();

    }

    void MoveTowardsPlayer()
    {

        nav.SetDestination(player.position);
        if (Vector3.Distance(transform.position, player.position) <= meleeRange)
        {
            nav.isStopped = true; 
        }
        else
        {
            nav.isStopped = false; 
        }
        /*if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            transform.LookAt(new Vector3(player.position.x, transform.position.y, transform.position.z));

        }*/
    }

    void TryAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (Time.time > lastAttackTime + attackCooldown)
        {
            if (canMeleeAttack && distanceToPlayer <= meleeRange)
            {
                MeleeAttack();
            }
            else if (canRangedAttack && distanceToPlayer <= rangedRange)
            {
                RangedAttack();
            }
        }
    }

    void MeleeAttack()
    {
        lastAttackTime = Time.time;
        Debug.Log("Enemy performs a melee attack!");

        PlayerHealth playerHealth =  player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(meleeDamage);
        }
    }

    void RangedAttack()
    {
        lastAttackTime = Time.time;
        Debug.Log("Enemy performs a ranged attack!");

        if (hiveAttackPrefab != null && hiveAttackSpawnPoint != null)
        {
            GameObject projectile = Instantiate(hiveAttackPrefab, hiveAttackSpawnPoint.position, hiveAttackSpawnPoint.rotation);

            // Set projectile properties if needed
            HiveProjectile hiveProjectile = projectile.GetComponent<HiveProjectile>();
            if (hiveProjectile != null)
            {
                hiveProjectile.player = player; // Assign the player reference to the projectile
                hiveProjectile.speed = hiveBulletSpeed; // Set projectile speed
            }
        }
        else
        {
            Debug.LogWarning("Projectile prefab or spawn point not assigned.");
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

    private void OnDrawGizmosSelected()
    {
        // Visualize melee range
        if (canMeleeAttack)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, meleeRange);
        }

        // Visualize ranged range
        if (canRangedAttack)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, rangedRange);
        }
    }
}

