using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Zapper : SpawnManager
{
    protected override void Spawn()
    {
        Transform randT = spawnTransforms[Random.Range(0, spawnTransforms.Length)];
        objectPooler.SpawnFromPool(objectPooler.playerZapperSO.pool.tag,
            new Vector3(randT.position.x, randT.position.y), Quaternion.identity);
    }
}
