using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    // Fields
    private int currentHealth;
    private int currentMaxHealth;
    
    // Properties
    public int health 
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
    public int MaxHealth
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
    public void Damage(int amount)
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
