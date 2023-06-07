using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)] 
    private PlayerStat playerStat;
    
    [Header(DS_Constants.ASSIGNABLE)]
    public GameManager gameManager;
    public GameObject levelUpPanel;
    public GameObject gameWinPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI XPText;
    public TextMeshProUGUI objectiveCounterText;
    public TextMeshProUGUI enemyCounterText;
    
    public OnUpdateUIXP onUpdateUIXP = new();
    
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        playerStat = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>();
        UpdateXPUI(playerStat.level, playerStat.currentXP, playerStat.requiredXP);
        UpdateEnemyCountUI(SingletonManager.Get<GameManager>().enemyCounter);
        UpdateObjectivesUI(null);
        
        foreach (GameObject go in gameManager.objectiveGos)
        {
            go.transform.GetChild(0).GetComponent<IO_Pool>().onInteractEvent.AddListener(UpdateObjectivesUI);
        }
    }

    private void OnEnable()
    {
        onUpdateUIXP.AddListener(UpdateXPUI);
        gameManager.onLevelUpEvent.AddListener(OpenLevelUpUI);
        gameManager.onPlayerWinEvent.AddListener(PlayerWinUI);
        gameManager.player.GetComponent<PlayerStat>().unitHealth.onDeathEvent.AddListener(PlayerLoseUI);
    }
    
    private void OnDisable()
    {
        onUpdateUIXP.RemoveListener(UpdateXPUI);
        gameManager.onLevelUpEvent.RemoveListener(OpenLevelUpUI);
        gameManager.onPlayerWinEvent.RemoveListener(PlayerWinUI);
        gameManager.player.GetComponent<PlayerStat>().unitHealth.onDeathEvent.RemoveListener(PlayerLoseUI);
        
        foreach (GameObject go in gameManager.objectiveGos)
        {
            go.transform.GetChild(0).GetComponent<IO_Pool>().onInteractEvent.RemoveListener(UpdateObjectivesUI);
        }
    }

    private void PlayerWinUI()
    {
        gameWinPanel.SetActive(true);
    }
    
    private void PlayerLoseUI()
    {
        gameOverPanel.SetActive(true);
    }

    private void UpdateObjectivesUI(GameObject ioPoolGameObject)
    {
        //Debug.Log(gameManager.objectiveCounter);
        objectiveCounterText.text = "Objective Counter: " + gameManager.objectiveCounter;
        ioPoolGameObject?.GetComponent<IO_Pool>().onInteractEvent.RemoveListener(UpdateObjectivesUI);
    }
    
    private void UpdateXPUI(int level, float currentXP, float requiredXP)
    {
        LevelText.text = "Lvl: " + level;
        XPText.text = "XP: " + currentXP + " / " + requiredXP;
    }

    public void UpdateEnemyCountUI(int enemyCount)
    {
        enemyCounterText.text = "Enemy Count: " + enemyCount;
    }

    private void OpenLevelUpUI(bool p_bool)
    {
        levelUpPanel.SetActive(p_bool);
    }
}
