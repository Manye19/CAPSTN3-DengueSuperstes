using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyListToSpawn Scriptable Object", menuName = "Scriptable Objects/EnemySpawnList")]
public class SO_EnemyListToSpawn : ScriptableObject
{
    public List<SO_Pool> enemiesToSpawn;
}
