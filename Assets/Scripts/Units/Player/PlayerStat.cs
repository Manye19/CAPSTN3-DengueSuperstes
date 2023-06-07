using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    protected override void Death()
    {
        base.Death();
        unitHealth.onDeathEvent.Invoke();
    }
    
    public override void GainExperienceFlatRate(float xpGained)
    {
        base.GainExperienceFlatRate(xpGained);
        UIManager uiManager = SingletonManager.Get<UIManager>();
        uiManager.onUpdateUIXP.Invoke(level, currentXP, requiredXP);
    }

    public override void GainExperienceScalable(float xpGained, int playerLevel, int enemyLevel)
    {
        base.GainExperienceScalable(xpGained, playerLevel, enemyLevel);
        UIManager uiManager = SingletonManager.Get<UIManager>();
        uiManager.onUpdateUIXP.Invoke(level, currentXP, requiredXP);
    }

    protected override void LevelUp()
    {
        base.LevelUp();
        GameManager gameManager = SingletonManager.Get<GameManager>();
        gameManager.onLevelUpEvent.Invoke(true);
    }
}
