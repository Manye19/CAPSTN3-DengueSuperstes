using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public OnDeathEvent onDeathEvent = new();
    
    // Fields
    private float currentHealth;
    private float currentMaxHealth;
    
    // Properties
    public float health 
    {
        get => currentHealth;
        set => currentHealth = value;
    }
    public float maxHealth
    {
        get => currentMaxHealth;
        set => currentMaxHealth = value;
    }
    
    // Constructor
    public Health(int health, int maxHealth)
    {
        currentHealth = health;
        currentMaxHealth = maxHealth;
    }
    
    // Methods
    public void Damage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        // Do something; an event maybe
        onDeathEvent.Invoke();
        // Debug.Log("Death() is called.");
    }
    
    public void Heal(float amount)
    {
        if (health < maxHealth)
        {
            health += amount;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void IncreaseHealth(int level)
    {
        maxHealth += (health * 0.01f) * ((100 - level) * 0.1f);
        //Debug.Log("Player's HP is now: " + health + "/" + maxHealth);
    }
}
