using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private ObjectPooler objectPooler;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private bool isHoldingDownKey;
    [SerializeField] private float buttonDownCounter;
    [SerializeField] private BoxCollider2D boxCollider2D;
    
    // Spawning Around Player Variables
    private int numberOfObjects = 20;
    private float spawnRadius = 20f;
    private float startAngle = 0f;
    private float endAngle = 360f;

    [Header(DS_Constants.ASSIGNABLE)]
    public float spawnTimerMin;
    public float spawnTimerMax;
    
    public float buttonHoldDownTime;
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
        ResetVariables();
        
        // Get player reference
        player = SingletonManager.Get<GameManager>().player;
        playerRb = player.GetComponent<Rigidbody2D>();
        
        // Get objectPooler reference
        objectPooler = SingletonManager.Get<ObjectPooler>();
        objectPooler.CreatePool(objectPooler.baseEnemySO);
        
        // Start Coroutine of Enemy Spawning
        StartCoroutine(SpawnCoroutine());
    }

    private void Update()
    {
        // Spawning_Wave_Type3
        // Spawn more circular
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float angleStep = (endAngle - startAngle) / numberOfObjects;
            for (int i = 0; i < numberOfObjects; i++)
            {
                float angle = Mathf.Deg2Rad * (startAngle + (i * angleStep));
                Vector2 spawnPosition = new Vector2(
                    player.transform.position.x + Mathf.Cos(angle) * spawnRadius,
                    player.transform.position.y + Mathf.Sin(angle) * spawnRadius
                );
                objectPooler.SpawnFromPool(objectPooler.baseEnemySO.pool.tag, spawnPosition, Quaternion.identity);

                // EnemyCounter++
                SingletonManager.Get<GameManager>().onEnemySpawnEvent.Invoke();
            }
        }

        if (Input.anyKey)
        {
            switch (playerRb.velocity.normalized)
            {
                case { x: > 0, y: > 0 }:
                    RandBoxCountdown(upperRightColliders);
                    break;
                case { x: < 0, y: > 0 }:
                    RandBoxCountdown(upperRightColliders);
                    break;
                case { x: < 0, y: < 0 }:
                    RandBoxCountdown(bottomRightColliders);
                    break;
                case { x: > 0, y: < 0 }:
                    RandBoxCountdown(bottomRightColliders);
                    break;
                case { x: > 0, y: 0 }:
                    RandBoxCountdown(rightColliders);
                    break;
                case { x: 0, y: > 0 }:
                    RandBoxCountdown(upColliders);
                    break;
                case { x: < 0, y: 0 }:
                    RandBoxCountdown(rightColliders);
                    break;
                case { x: 0, y: < 0 }:
                    RandBoxCountdown(bottomColliders);
                    break;
                /*case {x: 0, y: 0}:
                    ResetVariables();
                    break;
                default:
                    ResetVariables();
                    break;*/
            }
        }
        else if (Input.GetKeyUp(KeyCode.W)
            || Input.GetKeyUp(KeyCode.A)
            || Input.GetKeyUp(KeyCode.S)
            || Input.GetKeyUp(KeyCode.D))
        {
            ResetVariables();
        }
    }
    
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            float randNum = Random.Range(spawnTimerMin, spawnTimerMax);
            yield return new WaitForSeconds(randNum);
            SpawnEnemy();
            
            // EnemyCounter++
            SingletonManager.Get<GameManager>().onEnemySpawnEvent.Invoke();
        }
    }

    #region Functions
    private void SpawnEnemy()
    {
        if (!isHoldingDownKey)
        {
            spawnPos = GetRandSpawnCollidersPos();
        }
        else
        {
            spawnPos = GetRandBoxDirPos(boxCollider2D);
        }

        GameObject obj = objectPooler.SpawnFromPool(objectPooler.baseEnemySO.pool.tag, spawnPos, Quaternion.identity);
        
        // Update movement speed sample script
        // obj.GetComponent<EnemyMovement>().UpdateMovementSpeed(obj.GetComponent<EnemyStat>().movementSpeed);
    }

    private Vector3 GetRandSpawnCollidersPos()
    {
        int randNum = Random.Range(0, spawnColliders.Length);
        var randPoint = new Vector3(
            Random.Range(spawnColliders[randNum].bounds.min.x, spawnColliders[randNum].bounds.max.x),
            Random.Range(spawnColliders[randNum].bounds.min.y, spawnColliders[randNum].bounds.max.y),
            0f
        );
        return randPoint;
    }

    private Vector3 GetRandBoxDirPos(BoxCollider2D boxCollider2D)
    {
        var randPoint = new Vector3(
            Random.Range(boxCollider2D.bounds.min.x, boxCollider2D.bounds.max.x),
            Random.Range(boxCollider2D.bounds.min.y, boxCollider2D.bounds.max.y),
            0f
        );
        return randPoint;
    }

    private BoxCollider2D GetRandBoxCollider(BoxCollider2D[] boxCollider2Ds)
    {
        int randNum = Random.Range(0, boxCollider2Ds.Length);
        var boxCollider2D = boxCollider2Ds[randNum];
        return boxCollider2D;
    }

    private void RandBoxCountdown(BoxCollider2D[] boxCollider2Ds)
    {
        if (buttonDownCounter > 0)
        {
            buttonDownCounter -= Time.deltaTime;
        }
        else
        {
            isHoldingDownKey = true;
            boxCollider2D = GetRandBoxCollider(boxCollider2Ds);            
        }
    }

    private void ResetVariables()
    {
        boxCollider2D = null;
        isHoldingDownKey = false;
        buttonDownCounter = buttonHoldDownTime;
    }
    #endregion
}
