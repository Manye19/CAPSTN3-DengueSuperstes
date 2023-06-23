using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMagnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Pickup pickup))
        {
            pickup.SetTarget(transform.parent.position);
        }
    }
}
