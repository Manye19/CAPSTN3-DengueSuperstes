using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private ObjectPooler objectPooler;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D playerRb;

    [Header(DS_Constants.ASSIGNABLE)]
    public float spawnTimer;
    public BoxCollider2D[] spawnColliders;
    public BoxCollider2D[] leftColliders;
    public BoxCollider2D[] rightColliders;
    public BoxCollider2D[] upColliders;
    public BoxCollider2D[] bottomColliders;
    public BoxCollider2D[] upperLeftColliders;
    public BoxCollider2D[] upperRightColliders;
    public BoxCollider2D[] bottomLeftColliders;
    public BoxCollider2D[] bottomRightColliders;

    private void Start()
    {
        // Get player reference
        player = SingletonManager.Get<GameManager>().player;
        playerRb = player.GetComponent<Rigidbody2D>();
        
        // Get objectPooler reference
        objectPooler = SingletonManager.Get<ObjectPooler>();
        objectPooler.CreatePool(objectPooler.baseEnemySO);
        
        // Start Coroutine of Enemy Spawning
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            SpawnEnemy();
        }
    }

    #region Functions
    private void SpawnEnemy()
    {
        Vector3 position = GenerateRandomPosition();

        // Update Player position
        //position += player.transform.position;
        GameObject obj = objectPooler.SpawnFromPool(objectPooler.baseEnemySO.pool.tag, position);
        
        // Update movement speed sample script
        // obj.GetComponent<EnemyMovement>().UpdateMovementSpeed(obj.GetComponent<EnemyStat>().movementSpeed);
    }

    private Vector3 GenerateRandomPosition()
    {
        int randNum = Random.Range(0, spawnColliders.Length);
        var randPoint = new Vector3(
            Random.Range(spawnColliders[randNum].bounds.min.x, spawnColliders[randNum].bounds.max.x),
            Random.Range(spawnColliders[randNum].bounds.min.y, spawnColliders[randNum].bounds.max.y),
            0f
        );
        return randPoint;
    }
    #endregion
}
