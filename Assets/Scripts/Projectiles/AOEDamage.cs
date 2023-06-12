using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AOEDamage : Projectile
{
    [Header(DS_Constants.DO_NOT_ASSIGN)] 
    private Coroutine dotCoroutine;

    private Collider2D cc2D;
    
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
        cc2D = GetComponent<CircleCollider2D>();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyStat enemyStat))
        {
            //Debug.Log("OnTriggerEnter");
            Health enemyHealth = enemyStat.unitHealth;
            dotCoroutine = StartCoroutine(DamageOverTime(tickTime, enemyHealth));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<EnemyStat>())
        {
            if (dotCoroutine != null)
            {
                //Debug.Log("Stop DOT");
                StopCoroutine(dotCoroutine);
            }
        }
    }

    protected virtual IEnumerator DamageOverTime(float time, Health health)
    {
        //Debug.Log("DOT");
        health.Damage(tickDamage);
        cc2D.enabled = false;
        yield return new WaitForSeconds(time);
        cc2D.enabled = true;
    }
}
