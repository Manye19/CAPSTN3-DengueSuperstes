using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AOEDamage : Projectile
{
    [Header(DS_Constants.ASSIGNABLE)] 
    [SerializeField] private bool isDestruct;
    [SerializeField] private float tickDamage;
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
        if (other.GetComponent<EnemyStat>())
        {
            Health enemyHealth = other.GetComponent<EnemyStat>().unitHealth;
            StartCoroutine(DamageOverTime(tickTime, enemyHealth));
        }
    }
    
    protected virtual IEnumerator DamageOverTime(float time, Health health)
    {
        health.Damage(tickDamage);
        CircleCollider2D cc2D = GetComponent<CircleCollider2D>();
        cc2D.enabled = false;
        yield return new WaitForSeconds(time);
        cc2D.enabled = true;
    }
}
