using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPooler : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> poolDictionary = new();
    
    [Header("===== Editor: Assignable =====")]
    [Header("Pool Scriptable Objects")]
    public SO_PoolProjectile playerWhipSO;
    public SO_PoolProjectile playerSantaWaterSO;
    public SO_PoolProjectile playerDaggerSO;
    public SO_PoolProjectile playerInsecticideSO;
    public SO_PoolProjectile playerNetSO;
    public SO_PoolProjectile playerMosMagnetSO;
    public SO_PoolProjectile playerZapperSO;
    public EnemyScriptableObject baseEnemySO;
    public PickupScriptableObject expSO;

    private void Awake()
    {
        //_instance = this;
        SingletonManager.Register(this);
    }

    public void CreatePool(SO_Pool poolSO)
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();
        for (int i = 0; i < poolSO.pool.size; i++)
        {
            GameObject obj = Instantiate(poolSO.pool.prefab);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            objectPool.Enqueue(obj);
        }
        poolDictionary.Add(poolSO.pool.tag, objectPool);
    }
    
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + "does not exist.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
