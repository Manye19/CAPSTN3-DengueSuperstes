using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] protected bool isDestruct;
    [SerializeField] protected bool isMoving;
    protected float projectileSpeed;
    protected Vector2 projectileSize;
    protected float projectileDamage;
    protected float damageTimeTick;
    [SerializeField] protected SO_Pool poolSO; 
    protected float destructTimer;

    protected virtual void OnEnable()
    {
        UpdateProjectileStats();
        StartCoroutine((SelfDestructTimer()));
    }

    protected virtual void OnDisable()
    {
        UpdateProjectileStats();
    }

    protected virtual void Start()
    {
        // The disabled projectile won't get the updated stats.
        //SingletonManager.Get<GameManager>().onUpdateUpgradesEvent.AddListener(UpdateProjectileStats);
    }

    protected virtual void Update()
    {
        if (isMoving)
        {
            transform.Translate(0, projectileSpeed * Time.deltaTime, 0);
        }
    }

    protected virtual IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(destructTimer);
        //Debug.Log("Self Destruct Projectile.");
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col);
        if (col.gameObject.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.TakeDamage(projectileDamage, 0);
            // Debug.Log("Enemy took " + projectileDamage + " damage.");
        }
    }
    
    protected virtual void UpdateProjectileStats()
    {
        foreach (WeaponStat ws in SingletonManager.Get<WeaponsManager>().weapons)
        {
            if (ws.weaponLevelSO.name.Equals(poolSO.name))
            {
                transform.localScale.Set(ws.size.x, ws.size.y, ws.size.z);
                projectileSpeed = ws.speed;
                projectileDamage = ws.damage;
                damageTimeTick = ws.damageTick;
                destructTimer = ws.destructTimer;
            }
        }
        foreach (PowerupStat ps in SingletonManager.Get<PowerupsManager>().powerups)
        {
            switch (ps.powerupName)
            {
                case "Radius":
                {
                    Vector3 thisScale = transform.localScale;
                    thisScale.Set(
                        thisScale.x + ps.increase, 
                        thisScale.y + ps.increase, 
                        thisScale.z + ps.increase);
                    break;
                }
                case "Damage":
                    projectileDamage += ps.increase;
                    break;
                case "Speed":
                    projectileSpeed += ps.increase;
                    break;
                case "Time":
                    destructTimer += ps.increase;
                    break;
            }
        }
    }
}
