using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Stat : MonoBehaviour
{
    protected Health unitHealth = new Health(100, 100);

    [Header("===== Editor: Assignable =====")]
    [Header("Stats")] 
    public int health;
    public int maxHealth;
    public float atkSpeed;
    public float damage;
    public int defense;
    public float movementSpeed;

    public virtual void Start()
    {
        unitHealth.health = health;
        unitHealth.MaxHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        unitHealth.Damage(amount);
        if (unitHealth.health <= 0)
        {
            gameObject.SetActive(false); // Test, temporary
        }
    }

    public virtual void Heal(int amount)
    {
        unitHealth.Heal(amount);
    }
}
