using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : Enemy
{
    public float attackRange = 2f;
    public int damage = 25;

    public void Attack()
    {
        Debug.Log("MeleeEnemy attacks");

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= attackRange)
            {
                // Assuming the player has a method like TakeDamage(int amount)
                player.GetComponent<Health>()?.TakeDamage(damage);
            }
        }
    }
}
