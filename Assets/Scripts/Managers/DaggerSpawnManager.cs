using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DaggerSpawnManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private ObjectPooler objectPooler;

    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] private float daggerTimer;
    [SerializeField] private Transform[] daggerTransforms;
    [SerializeField] private Transform spawnT;

    private void OnEnable()
    {
        objectPooler = SingletonManager.Get<ObjectPooler>();
        objectPooler.CreatePool(objectPooler.playerDaggerSO);
        
        StartCoroutine(SpawnCoroutine());
    }

    private void LateUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                spawnT = daggerTransforms[5];
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                spawnT = daggerTransforms[5];
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                spawnT = daggerTransforms[2];
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                spawnT = daggerTransforms[3];
            }
            
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0)
            {
                //Debug.Log("diagonal working");
                spawnT = daggerTransforms[1];
            }
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0)
            {
                //Debug.Log("diagonal working");
                spawnT = daggerTransforms[1];
            }
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0)
            {
                //Debug.Log("diagonal working");
                spawnT = daggerTransforms[7];
            }
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
            {
                //Debug.Log("diagonal working");
                spawnT = daggerTransforms[7];
            }
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(daggerTimer);
            SpawnDagger();
        }
    }

    private void SpawnDagger()
    {
        GameObject go = objectPooler.SpawnFromPool(objectPooler.playerDaggerSO.pool.tag,
            new Vector3(spawnT.position.x, spawnT.position.y, 0f), spawnT.rotation);
        var speed = go.GetComponent<Dagger>().speed;
        go.GetComponent<Rigidbody2D>().velocity = spawnT.up * speed;
    }
}
