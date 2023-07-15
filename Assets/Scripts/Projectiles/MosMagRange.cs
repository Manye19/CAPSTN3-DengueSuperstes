using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MosMagRange : Projectile
{
    private List<EnemyMovement> enemyMovements = new();

    protected override void OnEnable()
    {
        if (isDestruct)
        {
            base.OnEnable();
        }
        UpdateProjectileStats();
    }

    private void OnDisable()
    {
        base.OnDisable();
        SingletonManager.Get<GameManager>().onChangeTargetEvent.Invoke(null);
        for (int i = 0; i < enemyMovements.Count; i++)
        {
            var em = enemyMovements[i];
            em.RemoveListenerToChangeTarget();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out EnemyMovement enemyMovement))
        {
            enemyMovements.Add(enemyMovement);
            enemyMovement.AddListenerToChangeTarget();
            SingletonManager.Get<GameManager>().onChangeTargetEvent.Invoke(transform.parent);
        }
    }
    
    protected override void UpdateProjectileStats()
    {
        foreach (WeaponStat ws in SingletonManager.Get<WeaponsManager>().weapons)
        {
            if (ws.weaponLevelSO.name.Equals(poolSO.name))
            {
                transform.localScale.Set(ws.size.x, ws.size.y, ws.size.z);
            }
        }
        foreach (PowerupStat ps in SingletonManager.Get<PowerupsManager>().powerups)
        {
            switch (ps.powerupName)
            {
                case "Radius":
                {
                    if (TryGetComponent(out CircleCollider2D cc2D))
                    {
                        cc2D.radius += ps.increase;
                    }
                    break;
                }
            }
        }
    }
}
