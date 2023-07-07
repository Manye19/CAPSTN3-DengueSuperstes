using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] private EnemyStat enemyStat;
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private float playerDef;
    [SerializeField] private float currentDamage;
    [SerializeField] private float currentAtkSpeed;
    
    private void Start()
    {
        playerDef = 0;
        currentDamage = enemyStat.statSO.damage;
        currentAtkSpeed = enemyStat.statSO.atkSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerStat playerStat))
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