using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)] 
    
    [Header(DS_Constants.ASSIGNABLE)]
    public GameManager gameManager;
    public GameObject levelUpPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI XPText;
    public TextMeshProUGUI enemyCounterText;
    
    public OnUpdateUIXP onUpdateUIXP = new();
    
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        PlayerStat playerStat = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>();
        UpdateUIXP(playerStat.level, playerStat.currentXP, playerStat.requiredXP);
        UpdateEnemyCountUI(SingletonManager.Get<GameManager>().enemyCounter);
    }

    private void OnEnable()
    {
        onUpdateUIXP.AddListener(UpdateUIXP);
        gameManager.onLevelUpEvent.AddListener(OpenLevelUpUI);
        gameManager.onPlayerDeath.AddListener(PlayerDeathUI);
    }
    
    private void OnDisable()
    {
        onUpdateUIXP.RemoveListener(UpdateUIXP);
        gameManager.onLevelUpEvent.RemoveListener(OpenLevelUpUI);
        gameManager.onPlayerDeath.RemoveListener(PlayerDeathUI);
    }

    private void PlayerDeathUI()
    {
        gameOverPanel.SetActive(true);
    }
    
    private void UpdateUIXP(int level, float currentXP, float requiredXP)
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
