using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AOEDamage : Projectile
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    public float katolSlowPercent;
    public float insecticideDefPercent;
    public float saltGunDefPercent;

    protected override void Start()
    {
        katolSlowPercent = GetComponent<EnemyStat>().enemyStatSO.katolSlowPercent;
        insecticideDefPercent = GetComponent<EnemyStat>().enemyStatSO.insecticideDefPercent;
        saltGunDefPercent = GetComponent<EnemyStat>().enemyStatSO.saltGunDefPercent;
    }

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
            enemyStat.StartDoT(projectileDamage, 0, damageTimeTick);
            enemyStat.DecrementMoveSpeed(katolSlowPercent);
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
