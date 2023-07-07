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
            //Debug.Log(ps.powerUpSO.levels[ps.level]);
            if (ps.powerUpSO)
            {
                ps.increase = ps.powerUpSO.levels[ps.level];
            }
        }
    }

    public void LevelUp(SO_PowerupLevels powerupSOReference)
    {
        foreach (PowerupStat ps in powerups)
        {
            if (ps.powerUpSO.Equals(powerupSOReference))
            {
                if (ps.level < 3)
                {
                    ps.level++;
                }
            }
        }
    }
}
