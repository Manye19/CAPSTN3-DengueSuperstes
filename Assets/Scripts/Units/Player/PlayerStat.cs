using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
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
}
