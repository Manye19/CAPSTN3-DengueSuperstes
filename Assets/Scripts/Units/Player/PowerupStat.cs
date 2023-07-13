using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class PowerupStat
{
    public string powerupName;
    [FormerlySerializedAs("powerUpSO")] public SO_PowerupLevels powerupLevelSO;
    
    public int level;
    public float increase;
}
