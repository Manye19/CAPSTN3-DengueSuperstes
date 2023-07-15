using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RewardsManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)] 
    private Dictionary<WeaponStat, PowerupStat> wppuDictionary = new();
    [SerializeField] private List<SO_Rewards> rewardsCache;

    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] private List<GameObject> buttonChoices;

    [SerializeField] private SO_Rewards pointsReward;
    [SerializeField] private List<SO_Rewards> staticRewardsList;

    private void Start()
    {
        SingletonManager.Get<GameManager>().onLevelUpEvent.AddListener(ScanItems);

        var wses = SingletonManager.Get<WeaponsManager>().weapons;
        var pses = SingletonManager.Get<PowerupsManager>().powerups;
        for (var i = 0; i < 7; i++)
        {
            wppuDictionary.Add(wses[i], pses[i]);
        }
    }
    
    private void OnDisable()
    {
        SingletonManager.Get<GameManager>().onLevelUpEvent.RemoveListener(ScanItems);
    }

    private void ScanItems(bool p_bool)
    {
        // Initialize and transfer all staticRewardsList to rewardsCache.
        foreach(SO_Rewards rw in staticRewardsList)
        {
            rewardsCache.Add(rw);
        }
        
        int activeWeaponCounter = 0;
        int maxLvlWeaponCounter = 0;
        int activePowerUpCounter = 0;
        int maxLevelPowerUpCounter = 0;

        foreach (var (ws, ps) in wppuDictionary)
        {
            if (ws.level >= 4)
            {
                maxLvlWeaponCounter++;
            }

            if (ps.level >= 3)
            {
                maxLevelPowerUpCounter++;
            }

            
            
            // Weapons
            if (ws.weaponSM.gameObject.activeInHierarchy)
            {
                // Active Weapon Counter
                activeWeaponCounter++;

                // If Player has Max Weapon Count (x5) 
                if (activeWeaponCounter >= 4)
                {
                    if (ws.isEvolved || ws.level >= 4)
                    {
                        rewardsCache.Remove(GetStaticReward(ws.weaponLevelSO, null));
                    }
                }
            }

            // Powerups
            if (ps.level >= 1)
            {
                // Active Powerup Counter
                activePowerUpCounter++;

                // If Player has Max Powerup Count (x5)
                if (activePowerUpCounter >= 4)
                {
                    if (ps.level >= 3)
                    {
                        rewardsCache.Remove(GetStaticReward(null, ps.powerupLevelSO));
                    }
                }
            }
            
            // Check for Pairings - HARDCODE
            // Instantly Offer Weapon level ups
            if (ws.level.Equals(2) && ps.level.Equals(3))
            {
                rewardsCache.Add(GetStaticReward(ws.weaponLevelSO, null));
            }
            else if (ws.level.Equals(3) && ps.level.Equals(3))
            {
                rewardsCache.Add(GetStaticReward(ws.weaponLevelSO, null));
            }
            else if (ws.level.Equals(4) && ps.level.Equals(3))
            {
                rewardsCache.Add(GetStaticReward(ws.weaponLevelSO, null));
            }
            
            // Instantly Offer Powerup level ups
            else if (ps.level.Equals(1) && ws.level.Equals(4))
            {
                rewardsCache.Add(GetStaticReward(null, ps.powerupLevelSO));
            }
            else if (ps.level.Equals(2) && ws.level.Equals(4))
            {
                rewardsCache.Add(GetStaticReward(null, ps.powerupLevelSO));
            }
        }
        
        // If all weapons and powerups are maxed out; offer points instead.
        if (maxLvlWeaponCounter >= 4 && maxLevelPowerUpCounter >= 4)
        {
            //staticRewardsList.Clear();
            rewardsCache.Clear();
            OfferPointsUI();
        }
        else
        {
            OfferRewardsUI();
        }
    }

    private void OfferRewardsUI()
    {
        int weaponLevel = 0;
        int powerupLevel = 0;
        
        for (int i = 0; i < buttonChoices.Count; i++)
        {
            int randNum = Random.Range(0, rewardsCache.Count);
            
            foreach (var (ws, ps) in wppuDictionary)
            {
                if (rewardsCache[randNum].weaponLevelSO)
                {
                    if (rewardsCache[randNum].weaponLevelSO.Equals(ws.weaponLevelSO))
                    {
                        weaponLevel = ws.level;
                        break;
                    }
                }
                else if (rewardsCache[randNum].powerupLevelSO)
                {
                    if (rewardsCache[randNum].powerupLevelSO.Equals(ps.powerupLevelSO))
                    {
                        powerupLevel = ps.level;
                        break;
                    }
                }
            }

            var j = GetStaticRewardIndex(rewardsCache[randNum]);

            // Reward Name.
            buttonChoices[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rewardsCache[randNum].rewardName;

            // Reward Sprite.
            // buttonChoices[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = rewardsCache.sprite;

            // RemoveAllListeners() to onClick.
            buttonChoices[i].GetComponent<Button>().onClick.RemoveAllListeners();

            if (rewardsCache[randNum].weaponLevelSO)
            {
                // Reward Description.
                buttonChoices[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = rewardsCache[randNum].rewardDescriptions[weaponLevel];
            
                // Check if "New!".
                buttonChoices[i].transform.GetChild(3).gameObject.SetActive(rewardsCache[randNum].isNew[weaponLevel]);
                
                // AddListener() to onClick.
                buttonChoices[i].GetComponent<Button>().onClick.AddListener(() => SingletonManager.Get<WeaponsManager>().LevelUp(staticRewardsList[j].weaponLevelSO));
                
                // Clear() when an option is chosen.
                buttonChoices[i].GetComponent<Button>().onClick.AddListener(() => rewardsCache.Clear());
            }
            if (rewardsCache[randNum].powerupLevelSO)
            {
                // Reward Description.
                buttonChoices[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = rewardsCache[randNum].rewardDescriptions[powerupLevel];
            
                // Check if "New!".
                buttonChoices[i].transform.GetChild(3).gameObject.SetActive(rewardsCache[randNum].isNew[powerupLevel]);
                
                // AddListener() to onClick.
                buttonChoices[i].GetComponent<Button>().onClick.AddListener(() => SingletonManager.Get<PowerupsManager>().LevelUp(staticRewardsList[j].powerupLevelSO));
                
                // Clear() when an option is chosen.
                buttonChoices[i].GetComponent<Button>().onClick.AddListener(() => rewardsCache.Clear());
            }
            
            // Remove them so as not to roll them again.
            rewardsCache.RemoveAt(randNum);
        }
    }

    private void OfferPointsUI()
    {
        for (int i = 0; i < buttonChoices.Count; i++)
        {
            // RemoveListeners();
            buttonChoices[i].GetComponent<Button>().onClick.RemoveAllListeners();
            
            // Reward Name.
            buttonChoices[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pointsReward.rewardName;
            
            // Reward Sprite.
            // buttonChoices[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = rewardsCache.sprite;
            
            // Reward Description.
            buttonChoices[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = pointsReward.rewardDescriptions[0];
            
            // Check if "New!".
            buttonChoices[i].transform.GetChild(3).gameObject.SetActive(false);
            
            // AddListener() to onClick.
            buttonChoices[i].GetComponent<Button>().onClick.AddListener(() => SingletonManager.Get<GameManager>().IncrementPoints(100));
        }
    }
    
    private SO_Rewards GetStaticReward(SO_WeaponLevel weaponLevelSO, SO_PowerupLevels powerupLevelSO)
    {
        if (weaponLevelSO)
        {
            foreach (var rw in staticRewardsList)
            {
                if (weaponLevelSO.Equals(rw.weaponLevelSO))
                {
                    return rw;
                }
            }
        }
        else if (powerupLevelSO)
        {
            foreach (var rw in staticRewardsList)
            {
                if (powerupLevelSO.Equals(rw.powerupLevelSO))
                {
                    return rw;
                }
            }
        }
        return null;
    }

    private int GetStaticRewardIndex(SO_Rewards rewardReference)
    {
        for (int i = 0; i < staticRewardsList.Count; i++)
        {
            if (rewardReference.Equals(staticRewardsList[i]))
            {
                return i;
            }
        }
        return -1;
    }
}