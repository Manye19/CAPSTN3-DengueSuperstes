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
            ws.speed = ws.weaponLevelSO.projectileSpeedLevels[ws.level];
            ws.size = ws.weaponLevelSO.projectileSizeLevels[ws.level];
            if (ws.weaponProjectile.GetComponent<CircleCollider2D>())
                ws.radius = ws.weaponLevelSO.radiusLevels[ws.level];
            else if (ws.weaponProjectile.GetComponent<BoxCollider2D>())
                ws.boxSize = ws.weaponLevelSO.boxSizeLevels[ws.level];
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
                    // If weapon is not activated yet and level is at 0
                    case false when ws.level.Equals(0):
                        Debug.Log(weaponSOReference.name + " acquired!");
                        ws.weaponSM.gameObject.SetActive(true);
                        break;
                    // Level 4 is the limit (basically Level 5)
                    case true when ws.level < 4:
                        Debug.Log(weaponSOReference.name + " leveled up!");
                        ws.level++;
                        break;
                }
            }
        }
    }
}
