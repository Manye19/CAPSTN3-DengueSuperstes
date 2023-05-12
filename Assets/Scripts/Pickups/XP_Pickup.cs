using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP_Pickup : Pickup
{
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerStat>())
        {
            col.GetComponent<PlayerStat>().GainExperienceFlatRate(amount);
        }
        base.OnTriggerEnter2D(col);
    }
}
