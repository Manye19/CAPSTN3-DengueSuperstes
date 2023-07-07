using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SM_Zapper : SpawnManager
{
    private int transformCounter = 0;

    protected override void Start()
    {
        transformCounter = spawnTransforms.Length - 1;
        base.Start();
    }

    protected override void Spawn()
    {
        Transform t = spawnTransforms[transformCounter];
        transformCounter--;
        GameObject go = objectPooler.SpawnFromPool(poolSO.pool.tag, new Vector3(t.position.x, t.position.y),
            Quaternion.identity);
        if (transformCounter <= 0)
        {
            transformCounter = spawnTransforms.Length - 1;
        }
    }
}
