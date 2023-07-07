using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insecticide : Projectile
{
    private List<EnemyStat> enemyStatList = new();
    protected override void OnEnable()
    {
        if (isDestruct)
        {
            base.OnEnable();
        }
        StartCoroutine(DamageAll());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (enemyStatList.Count > 0)
        {
            foreach (EnemyStat es in enemyStatList)
            {
                es.StopDoT();
            }
            enemyStatList.Clear();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyStat enemyStat))
        {
            // Make it so that they are independent coroutines
            //enemyStat.StartDoT(projectileDamage, enemyStat.enemyStatSO.insecticideDefPercent, damageTimeTick);
            enemyStatList.Add(enemyStat);
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyStat enemyStat))
        {
            //enemyStat.StopDoT();
            if (enemyStatList.Contains(enemyStat))
            {
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
                    es.TakeDamage(projectileDamage, es.insecticideDefPercent);
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
