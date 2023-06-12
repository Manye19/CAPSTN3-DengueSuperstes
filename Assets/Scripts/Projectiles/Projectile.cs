using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] protected float projectileDamage;

    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] protected float selfDestructTimer;
    
    protected virtual void OnEnable()
    {
        UpdateProjectileDamage();
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
        if (col.gameObject.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.TakeDamage(projectileDamage);
            // Debug.Log("Enemy took " + SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().damage + " damage.");
        }
    }

    public virtual void UpdateProjectileDamage()
    {
        projectileDamage = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().damage;
    }
}
