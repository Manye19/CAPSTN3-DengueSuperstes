using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    // Removed redundant overrides.

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        Debug.Log("Player's health is: " + unitHealth.health);
    }
}
