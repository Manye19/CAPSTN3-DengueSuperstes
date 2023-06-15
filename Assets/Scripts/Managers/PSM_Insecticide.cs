using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSM_Insecticide : ProjectileSpawnManager
{
    protected override void Start()
    {
        objectPooler = SingletonManager.Get<ObjectPooler>();
        soPoolProjectile = objectPooler.playerInsecticideSO;
        base.Start();
    }
}
