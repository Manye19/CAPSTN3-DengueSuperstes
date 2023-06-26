using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PSM_Salt : ProjectileSpawnManager
{
    protected override void Start()
    {
        objectPooler = SingletonManager.Get<ObjectPooler>();
        soPoolProjectile = objectPooler.playerDaggerSO;
        base.Start();
    }
}
