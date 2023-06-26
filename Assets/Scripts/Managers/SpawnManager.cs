using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    // DO Polymorphism to all GameObjects that use this kind of logic!!!

    [Header(DS_Constants.DO_NOT_ASSIGN)] 
    protected ObjectPooler objectPooler;

    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] protected SO_Pool poolSO;
    [SerializeField] protected Transform[] spawnTransforms;
    [SerializeField] protected float spawnRate;

    protected virtual void Start()
    {
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
        
    }
}
