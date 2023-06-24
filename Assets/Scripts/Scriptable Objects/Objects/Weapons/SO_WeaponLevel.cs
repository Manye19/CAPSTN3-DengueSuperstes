using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Weapon Level Scriptable Object", menuName = "Scriptable Objects/Weapon Level")]
public class SO_WeaponLevel : ScriptableObject
{
    public List<float> attackLevelRates;
}
