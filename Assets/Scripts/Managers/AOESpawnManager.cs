using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESpawnManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private ObjectPooler objectPooler;

    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] private Transform[] AOETransforms;
    [SerializeField] private float AOETimer;

    private void Start()
    {
        objectPooler = SingletonManager.Get<ObjectPooler>();
        objectPooler.CreatePool(objectPooler.playerSantaWaterSO);
        
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(AOETimer);
            SpawnAOE();
        }
    }

    private void SpawnAOE()
    {
        for (int i = 0; i < AOETransforms.Length; i++)
        {
            objectPooler.SpawnFromPool(objectPooler.playerSantaWaterSO.pool.tag, 
                new Vector3(AOETransforms[i].position.x, AOETransforms[i].position.y, 0f), Quaternion.identity);
        }
    }
}
