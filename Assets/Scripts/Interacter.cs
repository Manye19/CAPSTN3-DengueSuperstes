using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IO_Pool iObjPool))
        {
            //canInteract = true;
            GetComponentInParent<PlayerStat>().GainExperienceFlatRate(iObjPool.xpAmount);
            iObjPool.onInteractEvent.Invoke(iObjPool.gameObject);
        }
    }
}
