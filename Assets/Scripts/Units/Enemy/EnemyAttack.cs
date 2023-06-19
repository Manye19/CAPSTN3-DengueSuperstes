using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private float currentDamage;
    [SerializeField] private float attackTickSpeed;
    private Coroutine attackCoroutine;
    
    private void Start()
    {
        currentDamage = GetComponentInParent<EnemyStat>().damage;
        //attackTickSpeed = GetComponent<EnemyStat>().atkSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerStat playerStat))
        {
            //playerStat.StartDoT();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerStat>())
        {
            // Attack DoT (?)
            // Debug.Log("Player hit.");
            other.gameObject.GetComponent<PlayerStat>().TakeDamage(currentDamage);
        }
    }
}