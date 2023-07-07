using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class PowerupStat
{
    public string powerupName;
    public SO_PowerupLevels powerUpSO;
    
    public int level;
    public float increase;
}
