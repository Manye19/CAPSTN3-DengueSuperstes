using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private int currentDamage;
    
    private void Start()
    {
        currentDamage = GetComponent<EnemyStat>().damage;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerStat>())
        {
            col.gameObject.GetComponent<PlayerStat>().TakeDamage(currentDamage);
            Debug.Log("Player took " + currentDamage + " damage.");
        }
    }
}
