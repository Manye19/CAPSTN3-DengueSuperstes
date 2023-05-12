using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Pickup : Pickup
{
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerStat>())
        {
            col.GetComponent<PlayerStat>().Heal(amount);
        }
        base.OnTriggerEnter2D(col);
    }
}
