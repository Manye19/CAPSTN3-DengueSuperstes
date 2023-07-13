using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsManager : MonoBehaviour
{
    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] private GameManager gameManager;

    [Header(DS_Constants.DO_NOT_ASSIGN)]
    public List<PowerupStat> powerups;
    

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void OnEnable()
    {
        gameManager.onUpdateUpgradesEvent.AddListener(UpdatePowerupDatas);
    }
    
    private void OnDisable()
    {
        gameManager.onUpdateUpgradesEvent.RemoveListener(UpdatePowerupDatas);
    }

    private void UpdatePowerupDatas()
    {
        foreach (PowerupStat ps in powerups)
        {
            ps.increase = ps.powerupLevelSO.levels[ps.level];
            //Debug.Log(ps.increase);
        }
    }

    public void LevelUp(SO_PowerupLevels powerupLevelSOReference)
    {
        foreach (PowerupStat ps in powerups)
        {
            if (ps.powerupLevelSO.Equals(powerupLevelSOReference))
            {
                if (ps.level < 3)
                {
                    ps.level++;
                    Debug.Log(powerupLevelSOReference.name + " leveled up!");
                    break;
                }
            }
        }
        UpdatePowerupDatas();
    }
}
