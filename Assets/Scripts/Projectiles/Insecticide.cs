using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insecticide : Projectile
{
    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] private bool isDestruct;
    [SerializeField] private float tickTime;

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
            enemyStat.StartDoT(projectileDamage, enemyStat.enemyStatSO.insecticideDefPercent, tickTime);            
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.StopDoT();
        }
    }
}
