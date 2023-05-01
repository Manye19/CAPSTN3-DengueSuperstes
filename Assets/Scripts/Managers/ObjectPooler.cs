using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPooler : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> poolDictionary = new();
    
    [Header("Pool Scriptable Objects")]
    public ProjectileScriptableObject playerWhipSO;
    public EnemyScriptableObject baseEnemySO;

    private void Awake()
    {
        //_instance = this;
        SingletonManager.Register(this);
    }

    public void CreatePool(PoolScriptableObject poolSO)
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
    
    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + "does not exist.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}