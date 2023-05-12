using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    protected override void Death()
    {
        // Drop EXP pickup on death.
        SingletonManager.Get<PickupManager>().onExpDrop.Invoke(transform.position);
        base.Death();
    }
}
