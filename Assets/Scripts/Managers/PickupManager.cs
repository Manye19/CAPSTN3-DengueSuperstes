using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PickupManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    private ObjectPooler objectPooler;

    public OnExpDrop onExpDrop = new();

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void OnEnable()
    {
        onExpDrop.AddListener(SpawnPickup);
    }
    
    private void Start()
    {
        objectPooler = SingletonManager.Get<ObjectPooler>();
        objectPooler.CreatePool(objectPooler.expSO);
    }
    
    private void OnDisable()
    {
        onExpDrop.RemoveListener(SpawnPickup);
    }

    private void SpawnPickup(Vector3 pos)
    {
        GameObject obj = objectPooler.SpawnFromPool(objectPooler.expSO.pool.tag, pos, Quaternion.identity);
    }
}
