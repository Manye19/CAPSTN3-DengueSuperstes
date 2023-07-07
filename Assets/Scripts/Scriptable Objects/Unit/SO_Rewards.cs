using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Reward Level Scriptable Object", menuName = "Scriptable Objects/Reward Level")]
public class SO_Rewards : ScriptableObject
{
    public SO_WeaponLevel weaponLevelSO;
    public SO_PowerupLevels powerupLevelSO;
    public List<bool> isNew;
    public Sprite sprite;
    public string rewardName;
    [TextArea] public List<string> rewardDescriptions;
}
