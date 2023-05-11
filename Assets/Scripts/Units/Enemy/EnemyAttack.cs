using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private float currentDamage;
    //[SerializeField] private float attackTickSpeed;
    //private Coroutine attackCoroutine;
    
    private void Start()
    {
        currentDamage = GetComponent<EnemyStat>().damage;
        //attackTickSpeed = GetComponent<EnemyStat>().atkSpeed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerStat>())
        {
            // Attack DoT (?)
            other.gameObject.GetComponent<PlayerStat>().TakeDamage(currentDamage);
        }
    }
}