using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosMagnet : Projectile
{
    protected override IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(selfDestructTimer);
        SingletonManager.Get<GameManager>().onChangeTargetEvent.Invoke(null);
        transform.parent.gameObject.SetActive(false);
        yield break;
    }
}
