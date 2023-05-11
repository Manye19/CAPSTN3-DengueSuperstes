using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    protected override void Death()
    {
        base.Death();
        PlayerStat playerStat = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>();
        //playerStat.GainExperienceFlatRate(20);
        playerStat.GainExperienceScalable(20, playerStat.level, level);
        UIManager uiManager = SingletonManager.Get<UIManager>();
        uiManager.onUpdateUIXP.Invoke(playerStat.level, playerStat.currentXP, playerStat.requiredXP);
    }
}
