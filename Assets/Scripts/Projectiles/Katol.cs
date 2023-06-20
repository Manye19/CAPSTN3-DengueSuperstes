using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katol : Projectile
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
            enemyStat.StartDoT(projectileDamage, 0, tickTime);
            enemyStat.DecrementMoveSpeed(enemyStat.enemyStatSO.katolSlowPercent);
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
