using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    // Fields
    private float currentHealth;
    private float currentMaxHealth;
    
    // Properties
    public float health 
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }
    public float MaxHealth
    {
        get
        {
            return currentMaxHealth;
        }
        set
        {
            currentMaxHealth = value;
        }
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
        if (currentHealth > 0)
        {
            currentHealth -= amount;
        }
        else Death();
    }

    public void Death()
    {
        // Do something; an event maybe
        Debug.Log("Death() is called.");
    }
    
    public void Heal(int amount)
    {
        if (currentHealth < currentMaxHealth)
        {
            currentHealth += amount;
        }
        if (currentHealth > currentMaxHealth)
        {
            currentHealth = currentMaxHealth;
        }
    }
}
