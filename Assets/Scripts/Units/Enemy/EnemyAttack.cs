using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private EnemyStat enemyStat;
    [SerializeField] private float playerDef;
    [SerializeField] private float currentDamage;
    [SerializeField] private float currentAtkSpeed;
    
    private void Start()
    {
        enemyStat = GetComponentInParent<EnemyStat>();
        playerDef = 0;
        currentDamage = enemyStat.statSO.damage;
        currentAtkSpeed = enemyStat.statSO.atkSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PlayerStat playerStat))
        {
            playerStat.StartDoT(currentDamage, playerDef, currentAtkSpeed);            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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