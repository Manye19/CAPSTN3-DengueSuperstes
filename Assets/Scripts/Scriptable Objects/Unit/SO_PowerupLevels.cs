using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Powerup Level Scriptable Object", menuName = "Scriptable Objects/Powerup Level")]
public class SO_PowerupLevels : ScriptableObject
{
    public List<float> levels;
}
