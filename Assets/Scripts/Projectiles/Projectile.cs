using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header(DS_Constants.ASSIGNABLE)]
    public float projectileSpeed;
    [SerializeField] protected float projectileDamage;
    [SerializeField] protected float selfDestructTimer;
    
    protected virtual void OnEnable()
    {
        StartCoroutine((SelfDestructTimer()));
    }

    private IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(selfDestructTimer);
        //Debug.Log("Self Destruct Projectile.");
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col);
        if (col.gameObject.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.TakeDamage(projectileDamage);
            // Debug.Log("Enemy took " + projectileDamage + " damage.");
        }
    }

    protected virtual void UpdateProjectileDamage()
    {
        projectileDamage = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().damage;
    }
}
