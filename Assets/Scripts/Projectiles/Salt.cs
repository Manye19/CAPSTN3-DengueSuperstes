using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salt : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.TakeDamage(projectileDamage, 0);
            gameObject.SetActive(false);
        }
    }
}
