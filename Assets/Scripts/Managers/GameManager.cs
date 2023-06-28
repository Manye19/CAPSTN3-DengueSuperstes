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
    [SerializeField] private bool isGamePaused;
    private float startTime;
    private bool timerActive = false;
    public int enemyCounter;
    public int objectiveCounter;
    public List<GameObject> objectiveGos;
    
    [Header(DS_Constants.ASSIGNABLE)]
    public GameObject player;
    [SerializeField] private GameObject playerKatol;
    [SerializeField] private GameObject playerSantaWaterSpawner;
    [SerializeField] private GameObject objectivePrefab;
    [SerializeField] private List<Transform> objectiveTransformsList;

    public OnGamePauseEvent onGamePauseEvent = new();
    public OnPlayerWinEvent onPlayerWinEvent = new();
    public OnLevelUpEvent onLevelUpEvent = new();
    public OnEnemySpawnEvent onEnemySpawnEvent = new();
    public OnChangeTargetEvent onChangeTargetEvent = new();
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

    private void OnEnable()
    {
        onLevelUpEvent.AddListener(OnPlayerLevelUp);
        player.GetComponent<PlayerStat>().unitHealth.onDeathEvent.AddListener(OnPlayerLose);
        onEnemySpawnEvent.AddListener(AddOnEnemySpawn);
        onEnemyKill.AddListener(DecrementOnEnemyKill);
    }

    private void Start()
    {
        Time.timeScale = 1;
        startTime = Time.time;
    }

    private void OnDisable()
    {
        onLevelUpEvent.RemoveListener(OnPlayerLevelUp);
        //player.GetComponent<PlayerStat>().unitHealth.onDeathEvent.RemoveListener(OnPlayerLose);
        onEnemySpawnEvent.RemoveListener(AddOnEnemySpawn);
        onEnemyKill.AddListener(DecrementOnEnemyKill);
        
        foreach (GameObject go in objectiveGos)
        {
            if (go != null)
            {
                go.transform.GetChild(0).GetComponent<IO_Pool>().onInteractEvent.RemoveListener(OnPlayerWin);
            }
        }
    }

    private void Update()
    {
        if (!SingletonManager.Get<UIManager>().isMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                OnPauseGameTime(!isGamePaused);
                onGamePauseEvent.Invoke(isGamePaused);
            }
        }

        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        SingletonManager.Get<UIManager>().timerText.text = minutes + ":" + seconds;
    }

    public void OnPauseGameTime(bool p_bool)
    {
        Time.timeScale = p_bool ? 0 : 1;
        isGamePaused = p_bool;
        // Time.timeScale = Time.timeScale >= 1 ? 0 : 1;
    }

    private void OnPlayerLevelUp(bool p_bool)
    {
        OnPauseGameTime(p_bool);
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
