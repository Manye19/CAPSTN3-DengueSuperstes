using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class ProjectileSpawnManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] protected ObjectPooler objectPooler;
    [SerializeField] protected SO_PoolProjectile soPoolProjectile;
    [SerializeField] private Transform spawnT;

    
    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] private float projectileTimer;
    [SerializeField] private Transform[] projectileTransforms;

    protected virtual void Start()
    {
        objectPooler = SingletonManager.Get<ObjectPooler>();
        objectPooler.CreatePool(soPoolProjectile);
        StartCoroutine(SpawnCoroutine());
    }

    protected virtual void LateUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                spawnT = projectileTransforms[5];
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                spawnT = projectileTransforms[5];
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                spawnT = projectileTransforms[2];
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                spawnT = projectileTransforms[3];
            }
            
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0)
            {
                //Debug.Log("diagonal working");
                spawnT = projectileTransforms[1];
            }
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0)
            {
                //Debug.Log("diagonal working");
                spawnT = projectileTransforms[1];
            }
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0)
            {
                //Debug.Log("diagonal working");
                spawnT = projectileTransforms[7];
            }
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
            {
                //Debug.Log("diagonal working");
                spawnT = projectileTransforms[7];
            }
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        while (soPoolProjectile)
        {
            yield return new WaitForSeconds(projectileTimer);
            SpawnProjectile(soPoolProjectile);
        }
    }

    private void SpawnProjectile(SO_PoolProjectile soPoolProjectile)
    {
        GameObject go = objectPooler.SpawnFromPool(soPoolProjectile.pool.tag,
            new Vector3(spawnT.position.x, spawnT.position.y, 0f), spawnT.rotation);
        float speed = go.GetComponent<Projectile>().projectileSpeed;
        go.GetComponent<Rigidbody2D>().velocity = spawnT.up * go.GetComponent<Projectile>().projectileSpeed;
    }
}
