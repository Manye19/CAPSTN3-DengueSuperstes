using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapperSpawnManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)] 
    [SerializeField] private ObjectPooler objectPooler;

    [Header(DS_Constants.ASSIGNABLE)] 
    [SerializeField] private float spawnRate;
    [SerializeField] private Transform[] ZapperTransforms;

    private void Start()
    {
        objectPooler = SingletonManager.Get<ObjectPooler>();
        objectPooler.CreatePool(objectPooler.playerZapperSO);
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnZapper();
        }
    }

    private void SpawnZapper()
    {
        Transform randT = ZapperTransforms[Random.Range(0, ZapperTransforms.Length)];
        objectPooler.SpawnFromPool(objectPooler.playerZapperSO.pool.tag,
            new Vector3(randT.position.x, randT.position.y), Quaternion.identity);
    }
}
