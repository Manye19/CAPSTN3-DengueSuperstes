using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)] 
    protected bool isEvolved = false;
    protected int level = 0;
    [SerializeField] protected ObjectPooler objectPooler;
    [SerializeField] protected float spawnRate;
    protected Transform spawnT;

    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] protected SO_Pool poolSO;
    [SerializeField] protected Transform[] spawnTransforms;

    protected virtual void Start()
    {
        SingletonManager.Get<GameManager>().onUpdateUpgradesEvent.AddListener(UpdateWeapon);
        objectPooler = SingletonManager.Get<ObjectPooler>();
        objectPooler.CreatePool(poolSO);
        StartCoroutine(SpawnCoroutine());
    }

    protected virtual IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Spawn();
        }
    }
    
    protected virtual void Spawn()
    {
        Transform randT = spawnTransforms[Random.Range(0, spawnTransforms.Length)];
        GameObject go = objectPooler.SpawnFromPool(poolSO.pool.tag,
            new Vector3(randT.position.x, randT.position.y), Quaternion.identity);
    }

    protected virtual void UpdateWeapon()
    {
        foreach (WeaponStat ws in SingletonManager.Get<WeaponsManager>().weapons)
        {
            if (ws.weaponLevelSO.name.Equals(poolSO.name))
            {
                spawnRate = ws.attackRate;
                if (ws.level >= 4)
                {
                    Evolve();
                }
                break;
            }
        }
        foreach (PowerupStat ps in SingletonManager.Get<PowerupsManager>().powerups)
        {
            if (ps.powerupName.Equals("AtkRate"))
            {
                spawnRate -= ps.increase;
            }
        }
    }

    protected virtual void Evolve()
    {
        
    }
}