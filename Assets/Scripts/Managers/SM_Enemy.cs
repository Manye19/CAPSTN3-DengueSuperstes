using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SM_Enemy : SpawnManager
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    private Vector3 spawnPos;
    private bool isHoldingDownKey;
    private float buttonDownCounter;
    private BoxCollider2D boxCollider2D;

    // Spawning Around Player Variables
    private int numberOfObjects = 20;
    private float spawnRadius = 20f;
    private float startAngle = 0f;
    private float endAngle = 360f;

    [Header(DS_Constants.ASSIGNABLE)]
    public float spawnTimerMin;
    public float spawnTimerMax;

    public SO_Time waveTimeSO;
    public SO_Time clusterTimeSO;
    public List<SO_Pool> enemyPools;
    public List<SO_EnemyListToSpawn> enemyListToSpawns;
    public SO_Pool clusterPool;

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

    public BoxCollider2D[] clusterSpawns;
    public BoxCollider2D[] clusterTargets;
    
    private void OnDisable()
    {
        SingletonManager.Get<GameManager>().OnTimeCheckEvent.RemoveListener(SpawnWavesOnTime);
    }

    protected override void Start()
    {
        SingletonManager.Get<GameManager>().OnTimeCheckEvent.AddListener(SpawnWavesOnTime);
        ResetVariables();
        objectPooler = SingletonManager.Get<ObjectPooler>();
        foreach (SO_Pool sp in enemyPools)
        {
            objectPooler.CreatePool(sp);
        }
        objectPooler.CreatePool(clusterPool);
        //StartCoroutine(SpawnEnemyCoroutine(enemyPools[0]));
    }

    private void Update()
    {
        // Spawning_Wave_Type3
        // Spawn more circular
        /*if (Input.GetKeyDown(KeyCode.Space))
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
        }*/

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                //Debug.Log("right");
                RandBoxCountdown(upperRightColliders);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                //Debug.Log("left");
                RandBoxCountdown(upperRightColliders);
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                //Debug.Log("up");
                RandBoxCountdown(upColliders);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                //Debug.Log("bottom");
                RandBoxCountdown(bottomColliders);
            }
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0)
            {
                //Debug.Log("upper-right");
                RandBoxCountdown(upperRightColliders);
            }
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0)
            {
                //Debug.Log("upper-left");
                RandBoxCountdown(upperRightColliders);
            }
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0)
            {
                //Debug.Log("bottom-right");
                RandBoxCountdown(bottomRightColliders);
            }
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
            {
                //Debug.Log("bottom-left");
                RandBoxCountdown(bottomRightColliders);
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
    
    private IEnumerator SpawnEnemyCoroutine(List<SO_Pool> enemiesToSpawn)
    {
        while (true)
        {
            float randNum = Random.Range(spawnTimerMin, spawnTimerMax);
            yield return new WaitForSeconds(randNum);
            SpawnEnemy(enemiesToSpawn);
        }
    }

    private void SpawnEnemy(List<SO_Pool> enemiesToSpawn)
    {
        if (!isHoldingDownKey)
        {
            int randNum = Random.Range(0, enemiesToSpawn.Count - 1);
            Debug.Log(randNum);
            string randTag = enemiesToSpawn[randNum].pool.tag;
            Debug.Log(randTag);
            
            for (int i = 0; i < 1; i++)
            {
                spawnPos = GetRandSpawnCollidersPos();
                objectPooler.SpawnFromPool(randTag, spawnPos, Quaternion.identity);
                // EnemyCounter++
                SingletonManager.Get<GameManager>().onEnemySpawnEvent.Invoke();
            }
        }
        else
        {
            int randNum = Random.Range(0, enemiesToSpawn.Count - 1);
            Debug.Log(randNum);
            string randTag = enemiesToSpawn[randNum].pool.tag;
            Debug.Log(randTag);
            
            for (int i = 0; i < 1; i++)
            {
                spawnPos = GetRandBoxDirPos(boxCollider2D);
                objectPooler.SpawnFromPool(randTag, spawnPos, Quaternion.identity);
                // EnemyCounter++
                SingletonManager.Get<GameManager>().onEnemySpawnEvent.Invoke();
            }
        }
    }

    #region Functions
    private void SpawnWavesOnTime(float gameTime)
    {
        //Debug.Log(gameTime);
        for (var i = 0; i < waveTimeSO.timeStampList.Count; i++)
        {
            var tm = waveTimeSO.timeStampList[i];
            if (gameTime.Equals(tm))
            {
                StartSpawn(enemyListToSpawns[i].enemiesToSpawn);
            }
        }

        for (var i = 0; i < clusterTimeSO.timeStampList.Count; i++)
        {
            var tm = clusterTimeSO.timeStampList[i];
            if (gameTime.Equals(tm))
            {
                SpawnCluster();
            }
        }
    }

    private void StartSpawn(List<SO_Pool> enemiesToSpawn)
    {
        StopSpawn();
        StartCoroutine(SpawnEnemyCoroutine(enemiesToSpawn));
    }
    private void StopSpawn()
    {
        StopAllCoroutines();
    }

    private void SpawnCluster()
    {
        int randNum = Random.Range(0, clusterSpawns.Length);
        //Debug.Log(clusterSpawns[randNum].position);
        GameObject go = objectPooler.SpawnFromPool(clusterPool.pool.tag, clusterSpawns[randNum].bounds.max, Quaternion.identity);
        go.GetComponent<ClusterMovement>().SetTarget(clusterTargets[randNum].bounds.max);
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
        Debug.Log("Variables are reset.");
        boxCollider2D = null;
        isHoldingDownKey = false;
        buttonDownCounter = buttonHoldDownTime;
    }

    protected override void UpdateWeapon()
    {
        
    }
    #endregion
}
