using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public OnUpdateUIXP onUpdateUIXP = new();

    [Header(DS_Constants.DO_NOT_ASSIGN)] 
    
    [Header(DS_Constants.ASSIGNABLE)]
    public GameManager gameManager;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI XPText;
    public GameObject levelUpPanel;
    public GameObject gameOverPanel;
    
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        PlayerStat playerStat = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>();
        UpdateUIXP(playerStat.level, playerStat.currentXP, playerStat.requiredXP);
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

    private void OpenLevelUpUI(bool p_bool)
    {
        levelUpPanel.SetActive(p_bool);
    }
}
