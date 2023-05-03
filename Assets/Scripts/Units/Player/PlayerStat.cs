using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    public override void Start()
    {
        base.Start();
        // Debug.Log("Player's HP is " + health);
        // Debug.Log("Player's Max HP is " + health);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        Debug.Log("Player's health is: " + unitHealth.health);
    }

    public override void Heal(int amount)
    {
        unitHealth.Heal(amount);
    }
}
