using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class WeaponStat
{
    public string weaponName;
    public SpawnManager weaponSM;
    public PowerupStat PowerupStat;
    public SO_WeaponLevel weaponLevelSO;
    public Projectile weaponProjectile;

    public bool isEvolved;
    public int level;
    public float speed;
    public Vector3 size;
    public float damage;
    public float damageTick;
    public float attackRate;
    public float destructTimer;
}
