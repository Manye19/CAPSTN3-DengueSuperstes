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

    protected override void OnEnable()
    {
        unitHealth.ResetHP(statSO.health, statSO.maxHealth);
        atkSpeed = statSO.atkSpeed;
        damage = statSO.damage;
        moveSpeed = statSO.moveSpeed;
        ResetMoveSpeed();
    }

    protected override void OnDisable()
    {
        unitHealth.ResetHP(statSO.health, statSO.maxHealth);
    }

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
        base.Death();
        SingletonManager.Get<GameManager>().onEnemyKill.Invoke();
    }
}
