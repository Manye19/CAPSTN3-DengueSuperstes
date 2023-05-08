using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("===== Runtime: DO NOT Assign =====")]
    [SerializeField] private float currentDamage;
    //[SerializeField] private float attackTickSpeed;
    //private Coroutine attackCoroutine;
    
    private void Start()
    {
        currentDamage = GetComponent<EnemyStat>().damage;
        //attackTickSpeed = GetComponent<EnemyStat>().atkSpeed;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerStat>())
        {
            // Attack DoT (?)
            other.gameObject.GetComponent<PlayerStat>().TakeDamage(currentDamage);
        }
    }
}