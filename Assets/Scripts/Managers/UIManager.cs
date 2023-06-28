using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)] 
    private PlayerStat playerStat;
    public bool isMenuOpen;
    
    [Header(DS_Constants.ASSIGNABLE)]
    public GameManager gameManager;

    private HealthBar healthBar;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    
    public GameObject pausePanel;
    public GameObject levelUpPanel;
    public GameObject gameWinPanel;
    public GameObject gameOverPanel;
    
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI XPText;
    public TextMeshProUGUI objectiveCounterText;
    public TextMeshProUGUI enemyCounterText;
    
    public Material flashMaterial;
    public Color flashColor;
    public float flashDuration;
    
    public OnUpdateUIXP onUpdateUIXP = new();
    
    private void Awake()
    {
        SingletonManager.Register(this);
    }
    
    private void OnEnable()
    {
        healthBar = healthSlider.GetComponent<HealthBar>();
        healthBar.slider = healthSlider;
        playerStat = gameManager.player.GetComponent<PlayerStat>();

        foreach (GameObject go in gameManager.objectiveGos)
            go.transform.GetChild(0).GetComponent<IO_Pool>().onInteractEvent.AddListener(UpdateObjectivesUI);
        playerStat.unitHealth.onDamageEvent.AddListener(UpdateHPUI);
        onUpdateUIXP.AddListener(UpdateXPUI);
        gameManager.onGamePauseEvent.AddListener(PauseUI);
        gameManager.onLevelUpEvent.AddListener(OpenLevelUpUI);
        gameManager.onPlayerWinEvent.AddListener(PlayerWinUI);
        playerStat.unitHealth.onDeathEvent.AddListener(PlayerLoseUI);
    }

    private void Start()
    {
        isMenuOpen = false;
        healthBar.SetMaxHealth(playerStat.unitHealth.maxHealth);
        UpdateHPUI(playerStat.unitHealth.health);
        UpdateXPUI(playerStat.level, playerStat.currentXP, playerStat.requiredXP);
        UpdateEnemyCountUI(gameManager.enemyCounter);
        UpdateObjectivesUI(null);
    }

    private void OnDisable()
    {
        playerStat.unitHealth.onDamageEvent.RemoveListener(UpdateHPUI);
        onUpdateUIXP.RemoveListener(UpdateXPUI);
        gameManager.onGamePauseEvent.RemoveListener(PauseUI);
        gameManager.onLevelUpEvent.RemoveListener(OpenLevelUpUI);
        gameManager.onPlayerWinEvent.RemoveListener(PlayerWinUI);
        //gameManager.player.GetComponent<PlayerStat>().unitHealth.onDeathEvent.RemoveListener(PlayerLoseUI);
        
        foreach (GameObject go in gameManager.objectiveGos)
        {
            if (go != null)
            {
                go.transform.GetChild(0).GetComponent<IO_Pool>().onInteractEvent.RemoveListener(UpdateObjectivesUI);
            }            
        }
    }

    private void PauseUI(bool p_bool)
    {
        pausePanel.SetActive(p_bool);
        //isMenuOpen = p_bool;
    }
    
    private void PlayerWinUI()
    {
        gameWinPanel.SetActive(true);
        isMenuOpen = true;
    }
    
    private void PlayerLoseUI()
    {
        gameOverPanel.SetActive(true);
        isMenuOpen = false;
    }
    
    private void UpdateObjectivesUI(GameObject ioPoolGameObject)
    {
        //Debug.Log(gameManager.objectiveCounter);
        objectiveCounterText.text = "Objective Counter: " + gameManager.objectiveCounter;
        ioPoolGameObject?.GetComponent<IO_Pool>().onInteractEvent.RemoveListener(UpdateObjectivesUI);
    }

    private void UpdateHPUI(float currentHealth)
    {
        healthBar.SetHealth(currentHealth);
        healthText.text = "HP: " + currentHealth + " / " + playerStat.statSO.maxHealth;
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

    public void OpenLevelUpUI(bool p_bool)
    {
        levelUpPanel.SetActive(p_bool);
        isMenuOpen = p_bool;
    }
}
