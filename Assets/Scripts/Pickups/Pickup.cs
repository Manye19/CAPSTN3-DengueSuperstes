using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pickup : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    private Rigidbody2D rb;
    private bool hasTarget;
    private Vector3 targetPos;

    [Header(DS_Constants.ASSIGNABLE)] 
    public float amount;
    public float pickupSpeed;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDir = (targetPos - transform.position).normalized;
            rb.velocity = new Vector2(targetDir.x, targetDir.y) * pickupSpeed;
        }
    }

    public virtual void SetTarget(Vector3 pos)
    {
        targetPos = pos;
        hasTarget = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerStat>())
        {
            gameObject.SetActive(false);
        }
    }
}
