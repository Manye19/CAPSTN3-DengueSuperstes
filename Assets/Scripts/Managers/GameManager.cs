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
    private float gameTime;
    private float cacheStartTime;
    private bool timerActive = false;
    public int enemyCounter;
    public int objectiveCounter;
    public List<GameObject> objectiveGos;
    
    [Header(DS_Constants.ASSIGNABLE)]
    public GameObject player;
    [SerializeField] private GameObject objectivePrefab;
    [SerializeField] private List<Transform> objectiveTransformsList;

    public OnTimeCheckEvent OnTimeCheckEvent = new();
    public OnGamePauseEvent onGamePauseEvent = new();
    public OnPlayerWinEvent onPlayerWinEvent = new();
    public OnLevelUpEvent onLevelUpEvent = new();
    public OnUpdateUpgradesEvent onUpdateUpgradesEvent = new();
    public OnEnemySpawnEvent onEnemySpawnEvent = new();
    public OnChangeTargetEvent onChangeTargetEvent = new();
    public OnDeathEvent onEnemyKill = new();
    
    private void Awake()
    {
        SingletonManager.Register(this);
        SpawnObjectives();
        StartCoroutine(CheckTime());
    }

    private void OnEnable()
    {
        onLevelUpEvent.AddListener(PlayerLevelUp);
        player.GetComponent<PlayerStat>().unitHealth.onDeathEvent.AddListener(PlayerLose);
        onEnemySpawnEvent.AddListener(EnemySpawnIncrement);
        onEnemyKill.AddListener(DecrementOnEnemyKill);
    }

    private void Start()
    {
        Time.timeScale = 1;
        cacheStartTime = Time.time;
        
        onUpdateUpgradesEvent.Invoke();
    }

    private void OnDisable()
    {
        onLevelUpEvent.RemoveListener(PlayerLevelUp);
        //player.GetComponent<PlayerStat>().unitHealth.onDeathEvent.RemoveListener(OnPlayerLose);
        onEnemySpawnEvent.RemoveListener(EnemySpawnIncrement);
        onEnemyKill.AddListener(DecrementOnEnemyKill);
        
        foreach (GameObject go in objectiveGos)
        {
            if (go != null)
            {
                go.transform.GetChild(0).GetComponent<IO_Pool>().onInteractEvent.RemoveListener(PlayerWin);
            }
        }
    }

    private void Update()
    {
        if (!SingletonManager.Get<UIManager>().isMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGameTime(!isGamePaused);
                onGamePauseEvent.Invoke(isGamePaused);
            }
        }

        gameTime = Time.time - cacheStartTime;
        string minutes = ((int)gameTime / 60).ToString();
        string seconds = (gameTime % 60).ToString("f2");
        SingletonManager.Get<UIManager>().timerText.text = minutes + ":" + seconds;
    }

    #region Time
    private IEnumerator CheckTime()
    {
        while (true)
        {
            OnTimeCheckEvent.Invoke((int)gameTime);
            yield return new WaitForSeconds(1f);
        }
    }
    
    public void PauseGameTime(bool p_bool)
    {
        Time.timeScale = p_bool ? 0 : 1;
        isGamePaused = p_bool;
        // Time.timeScale = Time.timeScale >= 1 ? 0 : 1;
    }
    #endregion
    #region LevelUp
    private void PlayerLevelUp(bool p_bool)
    {
        PauseGameTime(p_bool);
    }

    public void UpdateUpgrades()
    {
        onUpdateUpgradesEvent.Invoke();
    }
    #endregion
    private void SpawnObjectives()
    {
        foreach (Transform transform in objectiveTransformsList)
        {
            GameObject go = Instantiate(objectivePrefab, transform);
            go.transform.SetParent(SingletonManager.Get<ObjectPooler>().transform);
            go.transform.GetChild(0).GetComponent<IO_Pool>().onInteractEvent.AddListener(PlayerWin);
            objectiveGos.Add(go);
        }
    }
    #region Win/Lose Conditions
    private void PlayerWin(GameObject go)
    {
        //Debug.Log("Objective turned off.");
        objectiveCounter++;
        Debug.Log(objectiveCounter);
        go.GetComponent<IO_Pool>().onInteractEvent.RemoveListener(PlayerWin);
        if (objectiveCounter >= objectiveTransformsList.Count)
        {
            //Debug.Log("Player wins!");
            onPlayerWinEvent.Invoke();
            Time.timeScale = 0;
        }
        
    }

    private void PlayerLose()
    {
        Time.timeScale = 0;
    }
    #endregion
    #region Enemy Events
    private void EnemySpawnIncrement()
    {
        enemyCounter++;
        SingletonManager.Get<UIManager>().UpdateEnemyCountUI(enemyCounter);
    }

    private void DecrementOnEnemyKill()
    {
        enemyCounter--;
        SingletonManager.Get<UIManager>().UpdateEnemyCountUI(enemyCounter);
    }
    #endregion
    #region Reset() and Quit()
    public void GameReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ApplicationQuit()
    {
        Application.Quit();
    }
    #endregion
}
