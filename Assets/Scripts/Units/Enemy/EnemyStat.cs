using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    [HideInInspector] public SO_EnemyStat enemyStatSO;
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    public float katolSlowPercent;
    public float insecticideDefPercent;
    public float saltGunDefPercent;

    public override void Start()
    {
        base.Start();
        enemyStatSO = (SO_EnemyStat)statSO;
        katolSlowPercent = enemyStatSO.katolSlowPercent;
        insecticideDefPercent = enemyStatSO.insecticideDefPercent;
        saltGunDefPercent = enemyStatSO.saltGunDefPercent;
    }

    protected override void Death()
    {
        // Drop EXP pickup on death.
        SingletonManager.Get<PickupManager>().onExpDrop.Invoke(transform.position);
        SingletonManager.Get<GameManager>().onEnemyKill.Invoke();
        base.Death();
    }
}
