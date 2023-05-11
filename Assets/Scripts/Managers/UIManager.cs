using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public OnUpdateUIXP onUpdateUIXP = new();
    
    [Header(DS_Constants.ASSIGNABLE)]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI XPText;
    
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
    }
    
    private void OnDisable()
    {
        onUpdateUIXP.RemoveListener(UpdateUIXP);
    }

    private void UpdateUIXP(int level, float currentXP, float requiredXP)
    {
        LevelText.text = "Lvl: " + level;
        XPText.text = "XP: " + currentXP + " / " + requiredXP;
    }
}
