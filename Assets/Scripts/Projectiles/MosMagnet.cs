using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MosMagnet : Projectile
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

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out EnemyStat enemyStat))
        {
            //enemyStat.StartDoT(projectileDamage, 0, damageTimeTick);
            enemyStatList.Add(enemyStat);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
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

    protected override IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(destructTimer);
        transform.parent.gameObject.SetActive(false);
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
                    // Don't forget to count for their resistances
                    es.TakeDamage(projectileDamage, 0);
                    if (!es.isActiveAndEnabled)
                    {
                        enemyStatList.Remove(es);
                    }
                }
            }
            yield return new WaitForSeconds(damageTimeTick);
        }
    }

    protected override void UpdateProjectileStats()
    {
        foreach (WeaponStat ws in SingletonManager.Get<WeaponsManager>().weapons)
        {
            if (ws.weaponLevelSO.name.Equals(poolSO.name))
            {
                damageTimeTick = ws.damageTick;
                destructTimer = ws.destructTimer;
            }
        }
        foreach (PowerupStat ps in SingletonManager.Get<PowerupsManager>().powerups)
        {
            switch (ps.powerupName)
            {
                case "Damage":
                    projectileDamage += ps.increase;
                    break;
                case "Time":
                    destructTimer += ps.increase;
                    break;
            }
        }
    }
}
