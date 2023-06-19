using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AOEDamage : Projectile
{
    [Header(DS_Constants.ASSIGNABLE)] 
    [SerializeField] private bool isDestruct;
    [SerializeField] private float tickTime;
    public float slowPercent;
    
    protected override void OnEnable()
    {
        if (isDestruct)
        {
            base.OnEnable();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyStat enemyStat))
        {
            // Make it so that they are independent coroutines
            enemyStat.StartDoT(projectileDamage, tickTime);
            enemyStat.DecrementMoveSpeed(slowPercent);
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.StopDoT();
            enemyStat.ResetMoveSpeed();
        }
    }
}
