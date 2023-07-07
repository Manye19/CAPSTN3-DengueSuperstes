using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RewardsManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private List<SO_Rewards> rewardsCache;
    
    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] private List<GameObject> buttonChoices;
    [SerializeField] private List<SO_Rewards> rewardsList;

    private void Start()
    {
        SingletonManager.Get<GameManager>().onLevelUpEvent.AddListener(ScanItems);
    }
    
    private void OnDisable()
    {
        SingletonManager.Get<GameManager>().onLevelUpEvent.RemoveListener(ScanItems);
    }

    private void ScanItems(bool p_bool)
    {
        int weaponLevel = 0;
        int powerupLevel = 0;
        int maxLvlWeaponCounter = 0;
        int maxPowerupCounter = 0;
        
        foreach (WeaponStat ws in SingletonManager.Get<WeaponsManager>().weapons)
        {
            if (ws.level >= 4)
            {
                maxLvlWeaponCounter++;
            }

            foreach (SO_Rewards rw in rewardsList)
            {
                if (ws.weaponSM.isActiveAndEnabled)
                {
                    if (ws.weaponLevelSO.Equals(rw.weaponLevelSO))
                    {
                        weaponLevel = ws.level;
                        break;
                    }
                }
            }
        }
        foreach (PowerupStat ps in SingletonManager.Get<PowerupsManager>().powerups)
        {
            if (ps.level >= 3)
            {
                maxPowerupCounter++;
            }

            foreach (SO_Rewards rw in rewardsList)
            {
                if (ps.level >= 1)
                {
                    if (ps.powerUpSO.Equals(rw.powerupLevelSO))
                    {
                        powerupLevel = ps.level;
                        break;
                    }
                }
            }
        }

        float randomValue = Random.value;
        if (maxLvlWeaponCounter >= 4 && maxPowerupCounter >= 3)
        {
            // OfferPoints();
        }
        else if (randomValue < 0.5f) // Weapon
        {
            OfferRewards(weaponLevel, true);
        }
        else if (randomValue > 0.5f) // Powerup
        {
            OfferRewards(powerupLevel, false);
        }
    }

    private void OfferRewards(int level, bool isWeapon)
    { 
        for (int i = 0; i < buttonChoices.Count; i++)
        {
            if (isWeapon)
            {
                // (Get a RNG reference).
                int randNum = Random.Range(0, 6);

                // Add it to the cache.
                rewardsCache.Add(rewardsList[randNum]);

                // Remove it to the static list.
                rewardsList.RemoveAt(randNum);
            }
            else // Powerup
            {
                // (Get a RNG reference).
                int randNum = Random.Range(7, rewardsList.Count);
                
                // Add it to the cache.
                rewardsCache.Add(rewardsList[randNum]);

                // Remove it to the static list.
                rewardsList.RemoveAt(randNum);
            }

            // Reward Name
            buttonChoices[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rewardsCache[i].rewardName;

            // Reward Sprite
            //buttonChoices[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = rewardsCache.sprite;

            // Reward Description
            buttonChoices[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = rewardsCache[i].rewardDescriptions[level];
            
            // Check if "New!"
            buttonChoices[i].transform.GetChild(3).gameObject.SetActive(rewardsCache[i].isNew[level]);

            // RemoveListener and AddListener to onClick.
            buttonChoices[i].GetComponent<Button>().onClick.RemoveAllListeners();
            buttonChoices[i].GetComponent<Button>().onClick.AddListener(() => SingletonManager.Get<UIManager>().OpenLevelUpUI(false));
            buttonChoices[i].GetComponent<Button>().onClick.AddListener(() => SingletonManager.Get<GameManager>().UpdateUpgrades());
            buttonChoices[i].GetComponent<Button>().onClick.AddListener(() => SingletonManager.Get<GameManager>().PauseGameTime(false));
            if (rewardsCache[i].weaponLevelSO)
            {
                buttonChoices[i].GetComponent<Button>().onClick.AddListener(() => SingletonManager.Get<WeaponsManager>().LevelUp(rewardsCache[i].weaponLevelSO));
            }
            else if (rewardsCache[i].powerupLevelSO)
            {
                buttonChoices[i].GetComponent<Button>().onClick.AddListener(() => SingletonManager.Get<PowerupsManager>().LevelUp(rewardsCache[i].powerupLevelSO));
            }
        }
        foreach (SO_Rewards rw in rewardsCache)
        {
            // Add it to the static list to re-roll, before clearing.
            rewardsList.Add(rw);
        }
        // Clear cache list.
        rewardsCache.Clear();
    }
}