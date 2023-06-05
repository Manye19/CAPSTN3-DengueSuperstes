using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dagger : Projectile
{
    private Rigidbody2D rb;
    public float speed;
    public float damage;
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<EnemyStat>())
        {
            col.gameObject.GetComponent<EnemyStat>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
