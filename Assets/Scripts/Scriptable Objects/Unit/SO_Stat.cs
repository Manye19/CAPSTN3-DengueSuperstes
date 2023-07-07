using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_Stat : ScriptableObject
{
    [Header(DS_Constants.DEFAULTS)]
    public int health;
    public int maxHealth;
    public float atkSpeed;
    public float damage;
    public float moveSpeed;
}