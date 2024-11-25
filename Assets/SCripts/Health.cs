using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth = 150;

    public int scoreValue = 50;
    public int moneyValue = 25;

    // Update is called once per frame
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
            currentHealth -= amount;
            Debug.Log(gameObject.name + " took " + amount + " damage. Remaining health: " + currentHealth);
            if (currentHealth <= 0)
            {
                Die();

            }

    }

    public void Heal(int amount)
    {

    }

    public void Die()
    {
    
            if (Score.instance != null)
            {
                Score.instance.AddScore(scoreValue);
                Score.instance.AddMoney(moneyValue);
            }
            Destroy(gameObject);
        
  


        
    }
}
