using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] protected float selfDestructTimer;
    
    protected virtual void OnEnable()
    {
        StartCoroutine((SelfDestructTimer()));
    }

    private IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(selfDestructTimer);
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col);
        if (col.gameObject.GetComponent<EnemyStat>())
        {
            col.gameObject.GetComponent<EnemyStat>().TakeDamage(SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().damage);
            // Debug.Log("Enemy took " + SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().damage + " damage.");
        }
    }
}
