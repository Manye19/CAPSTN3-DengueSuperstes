using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Weapon Level Scriptable Object", menuName = "Scriptable Objects/Weapon Level")]
public class SO_WeaponLevel : ScriptableObject
{
    public List<float> projectileSpeedLevels;
    public List<Vector2> projectileSizeLevels;
    public List<float> radiusLevels;
    public List<Vector2> boxSizeLevels;
    public List<float> damageLevels;
    public List<float> damageTickLevels;
    public List<float> spawnRateLevels;
    public List<float> destructTimerLevels;
}
