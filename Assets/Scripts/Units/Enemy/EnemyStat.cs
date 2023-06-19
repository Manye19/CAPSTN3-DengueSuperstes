using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    [Header(DS_Constants.RESISTANCES)]
    public float insecticideDefPercent;
    
    protected override void Death()
    {
        // Drop EXP pickup on death.
        SingletonManager.Get<PickupManager>().onExpDrop.Invoke(transform.position);
        SingletonManager.Get<GameManager>().onEnemyKill.Invoke();
        base.Death();
    }
}
