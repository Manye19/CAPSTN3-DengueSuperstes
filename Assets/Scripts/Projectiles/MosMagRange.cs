using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosMagRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyMovement enemyMovement))
        {
            enemyMovement.ListenToChangeTarget();
            SingletonManager.Get<GameManager>().onChangeTargetEvent.Invoke(transform.parent);
        }
    }
}
