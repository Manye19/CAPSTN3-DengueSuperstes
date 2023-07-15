using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public List<WeaponStat> weapons;
    
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void OnEnable()
    {
        gameManager.onUpdateUpgradesEvent.AddListener(UpdateWeaponDatas);
    }
    
    private void OnDisable()
    {
        gameManager.onUpdateUpgradesEvent.RemoveListener(UpdateWeaponDatas);
    }

    private void UpdateWeaponDatas()
    {
        foreach (WeaponStat ws in weapons)
        {
            if (ws.weaponSM)
            {
                ws.isEvolved = ws.weaponSM.isEvolved;
            }
            ws.speed = ws.weaponLevelSO.projectileSpeedLevels[ws.level];
            ws.size = ws.weaponLevelSO.projectileSizeLevels[ws.level];
            ws.damage = ws.weaponLevelSO.damageLevels[ws.level];
            ws.damageTick = ws.weaponLevelSO.damageTickLevels[ws.level];
            ws.attackRate = ws.weaponLevelSO.spawnRateLevels[ws.level];
            ws.destructTimer = ws.weaponLevelSO.destructTimerLevels[ws.level];
        }
    }

    public void LevelUp(SO_WeaponLevel weaponSOReference)
    {
        foreach (WeaponStat ws in weapons)
        {
            if (ws.weaponLevelSO.Equals(weaponSOReference))
            {
                switch (ws.weaponSM.isActiveAndEnabled)
                {
                    // If weapon is not activated yet and weapon level should be at 0
                    case false when ws.level.Equals(0):
                        Debug.Log(weaponSOReference.name + " acquired!");
                        ws.weaponSM.gameObject.SetActive(true);
                        break;
                    // Level 5 is the limit (basically Level 6 is Evolution)
                    case true when ws.level < 5:
                        Debug.Log(weaponSOReference.name + " leveled up!");
                        ws.level++;
                        break;
                }
            }
        }
        UpdateWeaponDatas();
    }
}
