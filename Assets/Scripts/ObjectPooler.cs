using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // private static ObjectPooler _instance;
    // public static ObjectPooler instance
    // {
    //     get
    //     {
    //         if (_instance == null)
    //         {
    //             _instance = GameObject.FindObjectOfType<ObjectPooler>();
    //         }
    //         return _instance;
    //     }
    // }

    public List<GameObject> pooledObjects = new();
    
    private void Awake()
    {
        //_instance = this;
        SingletonManager.Register(this);
    }

    public void CreatePool(GameObject objectPrefab, int amountToPool)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject go = Instantiate(objectPrefab);
            go.SetActive(false);
            go.transform.SetParent(transform);
            pooledObjects.Add(go);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
