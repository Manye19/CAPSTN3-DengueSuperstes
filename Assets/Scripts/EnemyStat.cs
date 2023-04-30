using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        Debug.Log("Enemy health is " + unitHealth.health);
    }

    public override void Heal(int amount)
    {
        unitHealth.Heal(amount);
    }
}
