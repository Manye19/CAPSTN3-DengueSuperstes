using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    private float startTime;
    private bool timerActive = false;
    public int enemyCounter;
    public int objectiveCounter;
    
    [Header(DS_Constants.ASSIGNABLE)]
    public GameObject player;
    public GameObject playerGarlic;
    public GameObject playerSantaWaterSpawner;
    public GameObject objectivePrefab;
    public List<Transform> objectiveTransformsList;
    public List<GameObject> objectiveGos;

    public OnPlayerWinEvent onPlayerWinEvent = new();
    public OnLevelUpEvent onLevelUpEvent = new();
    public OnEnemySpawn onEnemySpawn = new();
    public OnDeathEvent onEnemyKill = new();
    
    private void Awake()
    {
        SingletonManager.Register(this);
        
        // Spawn Objectives
        foreach (Transform transform in objectiveTransformsList)
        {
            GameObject go = Instantiate(objectivePrefab, transform);
            go.transform.SetParent(SingletonManager.Get<ObjectPooler>().transform);
            go.transform.GetChild(0).GetComponent<IO_Pool>().onInteractEvent.AddListener(OnPlayerWin);
            objectiveGos.Add(go);
            objectiveCounter++;
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        startTime = Time.time;
    }

    private void OnEnable()
    {
        onLevelUpEvent.AddListener(PauseGameTime);
        player.GetComponent<PlayerStat>().unitHealth.onDeathEvent.AddListener(OnPlayerLose);
        onEnemySpawn.AddListener(AddOnEnemySpawn);
        onEnemyKill.AddListener(DecrementOnEnemyKill);
    }

    private void OnDisable()
    {
        onLevelUpEvent.RemoveListener(PauseGameTime);
        player?.GetComponent<PlayerStat>().unitHealth.onDeathEvent.RemoveListener(OnPlayerLose);
        onEnemySpawn.RemoveListener(AddOnEnemySpawn);
        onEnemyKill.AddListener(DecrementOnEnemyKill);
        
        foreach (GameObject go in objectiveGos)
        {
            go.transform.GetChild(0).GetComponent<IO_Pool>().onInteractEvent.RemoveListener(OnPlayerWin);
        }
    }

    private void Update()
    {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        SingletonManager.Get<UIManager>().timerText.text = minutes + ":" + seconds;
    }

    public Transform GetRandTransformFromList(List<Transform> list)
    {
        int randNum = Random.Range(0, list.Count);
        return list[randNum];
    }

    public void PauseGameTime(bool p_bool)
    {
        Time.timeScale = p_bool ? 0 : 1;
        //Time.timeScale = Time.timeScale >= 1 ? 0 : 1;
    }

    private void OnPlayerWin(GameObject go)
    {
        //Debug.Log("Objective turned off.");
        objectiveCounter--;
        go.GetComponent<IO_Pool>().onInteractEvent.RemoveListener(OnPlayerWin);
        if (objectiveCounter <= 0)
        {
            //Debug.Log("Player wins!");
            onPlayerWinEvent.Invoke();
            Time.timeScale = 0;
        }
    }

    private void OnPlayerLose()
    {
        Time.timeScale = 0;
    }

    public void AddOnEnemySpawn()
    {
        enemyCounter++;
        SingletonManager.Get<UIManager>().UpdateEnemyCountUI(enemyCounter);
    }

    public void DecrementOnEnemyKill()
    {
        enemyCounter--;
        SingletonManager.Get<UIManager>().UpdateEnemyCountUI(enemyCounter);
    }

    public void GameReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
