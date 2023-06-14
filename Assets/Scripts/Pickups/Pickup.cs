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
    private Vector2 targetPos;

    [Header(DS_Constants.ASSIGNABLE)] 
    public float amount;
    public float pickupSpeed;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnDisable()
    {
        ResetTarget();
    }

    protected virtual void FixedUpdate()
    {
        /*if (hasTarget)
        {
            Vector2 targetDir = (targetPos - transform.position).normalized;
            rb.velocity = new Vector2(targetDir.x, targetDir.y) * pickupSpeed;
        }*/

        if (hasTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, pickupSpeed * Time.deltaTime);
        }
    }

    public void SetTarget(Vector3 pos)
    {
        targetPos = pos;
        hasTarget = true;
    }

    private void ResetTarget()
    {
        targetPos = Vector2.zero;
        hasTarget = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerStat>())
        {
            gameObject.SetActive(false);
        }
    }
}
