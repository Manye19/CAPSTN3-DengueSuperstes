using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dagger : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.TakeDamage(projectileDamage);
            gameObject.SetActive(false);
        }
    }
}
