using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : Projectile
{
    public float speed;
    public float damage;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
