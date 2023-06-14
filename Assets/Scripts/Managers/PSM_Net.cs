using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSM_Net : ProjectileSpawnManager
{
    protected override void Start()
    {
        objectPooler = SingletonManager.Get<ObjectPooler>();
        soPoolProjectile = objectPooler.playerNetSO;
        base.Start();
    }
}
