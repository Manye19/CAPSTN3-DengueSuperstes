using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private ObjectPooler objectPooler;
    private GameObject player;
    
    public Vector2 spawnArea;
    public float spawnTimer;

    private void Start()
    {
        // Get player reference
        player = SingletonManager.Get<GameManager>().player;
        
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
        position += player.transform.position;
        GameObject obj = objectPooler.SpawnFromPool("Enemy", position);
        
        // Update movement speed sample script
        // obj.GetComponent<EnemyMovement>().UpdateMovementSpeed(obj.GetComponent<EnemyStat>().movementSpeed);
    }

    // Seems to be working HAHA!
    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new();
        // Randomizes and checks if negative or positive side will be used. | X = 20 | Y = 12 |
        float f = Random.value > 0.5f ? -1f : 1f;
        
        // Randomizes and checks if random value is more than 0.5f then we will use Y position as a max value.
        if (Random.value > 0.5f)
        {
            position.x = Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }
        position.z = 0;
        return position;
    }
    
    // Playing with more efficient "Off-Screen-Random-Position-Getter"
    /*private Vector3 GenerateRandomPosition()
    {
        // Vector3 position = new();
        float screenHeight = camera.orthographicSize * 2f;
        float screenWidth = screenHeight * camera.aspect;

        float randomX = Random.Range(camera.transform.position.x - screenWidth, camera.transform.position.x + screenWidth);
        float randomY = Random.Range(camera.transform.position.y - screenHeight, camera.transform.position.y + screenHeight);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        // position.x = Random.Range(-screenWidth, screenWidth);
        // position.y = Random.Range(-screenHeight, screenHeight);
        return spawnPosition;
    }*/
    #endregion
}
