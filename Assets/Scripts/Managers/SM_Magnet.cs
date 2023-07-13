using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Magnet : SpawnManager
{
    private int transformCounter = 0;

    protected override void Spawn()
    {
        for (int i = 0; i < spawnDuplicator; i++)
        {
            Transform t = spawnTransforms[transformCounter];
            transformCounter++;
            GameObject go = objectPooler.SpawnFromPool(poolSO.pool.tag, new Vector3(t.position.x, t.position.y),
                Quaternion.identity);
            if (transformCounter >= spawnTransforms.Length)
            {
                transformCounter = 0;
            }
        }
    }
}
