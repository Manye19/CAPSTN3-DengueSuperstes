using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katol : Projectile
{
    public List<EnemyStat> enemyStatList = new();
    
    protected override void OnEnable()
    {
        if (isDestruct)
        {
            base.OnEnable();
        }
        UpdateProjectileStats();
        StartCoroutine(DamageAll());
    }

    protected override void OnDisable()
    {
        UpdateProjectileStats();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyStat enemyStat))
        {
            // Make it so that they are independent coroutines
            /*enemyStat.StartDoT(projectileDamage, 0, damageTimeTick);
            enemyStat.DecrementMoveSpeed(enemyStat.enemyStatSO.katolSlowPercent);*/
            enemyStatList.Add(enemyStat);
            // Evolve - 30% chance - 90% move decrease affects all?
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyStat enemyStat))
        {
            /*enemyStat.StopDoT();
            enemyStat.ResetMoveSpeed();*/
            if (enemyStatList.Contains(enemyStat))
            {
                enemyStat.ResetMoveSpeed();
                enemyStatList.Remove(enemyStat);
            }
        }
    }

    private IEnumerator DamageAll()
    {
        while (true)
        {
            for (var index = 0; index < enemyStatList.Count; index++)
            {
                var es = enemyStatList[index];
                if (es.isActiveAndEnabled)
                {
                    es.TakeDamage(projectileDamage, 0);
                    es.DecrementMoveSpeed(es.enemyStatSO.katolSlowPercent);
                    if (!es.isActiveAndEnabled)
                    {
                        enemyStatList.Remove(es);
                    }
                }
            }
            yield return new WaitForSeconds(damageTimeTick);
        }
    }
}
