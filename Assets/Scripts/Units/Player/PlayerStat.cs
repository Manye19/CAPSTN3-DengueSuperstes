using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    public override void Start()
    {
        base.Start();
        level = 1;
        requiredXP = CalculateRequiredXP();
        SingletonManager.Get<GameManager>().onUpdateUpgradesEvent.AddListener(UpdatePlayerStat);
    }

    protected override void OnDisable()
    {
        unitHealth.onDeathEvent.RemoveListener(Death);
    }

    public override void GainExperienceFlatRate(float xpGained)
    {
        base.GainExperienceFlatRate(xpGained);
        UIManager uiManager = SingletonManager.Get<UIManager>();
        uiManager.onUpdateUIXP.Invoke(level, currentXP, requiredXP);
    }

    protected override void GainExperienceScalable(float xpGained, int playerLevel, int enemyLevel)
    {
        base.GainExperienceScalable(xpGained, playerLevel, enemyLevel);
        UIManager uiManager = SingletonManager.Get<UIManager>();
        uiManager.onUpdateUIXP.Invoke(level, currentXP, requiredXP);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        GameManager gameManager = SingletonManager.Get<GameManager>();
        gameManager.onLevelUpEvent.Invoke(true);
    }

    private void UpdatePlayerStat()
    {
        foreach (PowerupStat ps in SingletonManager.Get<PowerupsManager>().powerups)
        {
            if (ps.powerupName.Equals("MaxHP"))
            {
                unitHealth.maxHealth += ps.increase;
            }
        }
        SingletonManager.Get<UIManager>().UpdateHPUI(unitHealth.health);
    }

    public void SetPlayerStat(SO_PlayerStat playerStatSO)
    {
        statSO = playerStatSO;
    }
}
