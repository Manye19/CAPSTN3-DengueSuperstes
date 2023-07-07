using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MosMagRange : MonoBehaviour
{
    private List<EnemyMovement> enemyMovements = new();

    private void OnDisable()
    {
        SingletonManager.Get<GameManager>().onChangeTargetEvent.Invoke(null);
        for (int i = 0; i < enemyMovements.Count; i++)
        {
            var em = enemyMovements[i];
            em.RemoveListenerToChangeTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyMovement enemyMovement))
        {
            enemyMovements.Add(enemyMovement);
            enemyMovement.AddListenerToChangeTarget();
            SingletonManager.Get<GameManager>().onChangeTargetEvent.Invoke(transform.parent);
        }
    }
}
