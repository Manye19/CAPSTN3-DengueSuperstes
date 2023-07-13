using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterAttack : EnemyAttack
{
    [SerializeField] private SO_EnemyStat enemyStatSO;

    protected override void Start()
    {
        playerDef = 0;
        currentDamage = enemyStatSO.damage;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerStat playerStat))
        {
            playerStat.TakeDamage(currentDamage, playerDef);
        }
    }
}
