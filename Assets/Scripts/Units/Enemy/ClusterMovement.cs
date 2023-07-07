using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterMovement : EnemyMovement
{
    protected override void OnEnable()
    {
        
    }

    protected override void Start()
    {
        currentSpeed = enemyStat.statSO.moveSpeed;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override IEnumerator UpdatePlayerLocation()
    {
        return null;
    }

    public override void AddListenerToChangeTarget()
    {
        
    }

    public override void RemoveListenerToChangeTarget()
    {
        
    }

    protected override void ChangeTarget(Transform target)
    {
        
    }

    public override void UpdateMovementSpeed(float moveSpeed)
    {
        base.UpdateMovementSpeed(moveSpeed);
    }
}
