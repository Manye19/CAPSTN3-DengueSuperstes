using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] protected EnemyStat enemyStat;
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] protected float playerDef;
    [SerializeField] protected float currentDamage;
    [SerializeField] protected float currentAtkSpeed;
    
    protected virtual void Start()
    {
        playerDef = 0;
        currentDamage = enemyStat.statSO.damage;
        currentAtkSpeed = enemyStat.statSO.atkSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerStat playerStat))
        {
            playerStat.StartDoT(currentDamage, playerDef, currentAtkSpeed);            
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStat playerStat))
        {
            playerStat.StopDoT();
        }
    }

    public void UpdateAttackSpeed(float atkSpeed)
    {
        currentAtkSpeed = atkSpeed;
    }
}