using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterMovement : EnemyMovement
{
    [SerializeField] private SO_EnemyStat enemyStatSO;
    protected override void OnEnable()
    {
        
    }

    protected override void Start()
    {
        
        enemyStat = GetComponent<EnemyStat>();
        currentSpeed = enemyStatSO.moveSpeed;
    }

    protected override void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime); // temporary for now
        if (transform.position.x >= targetPos.x && transform.position.y >= targetPos.y)
        {
            gameObject.SetActive(false);
        }
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

    public override void ChangeTarget(Transform target)
    {
        
    }

    public void SetTarget(Vector3 target)
    {
        Debug.Log(target + " is set.");
        targetPos = target;
    }
    
    public override void UpdateMovementSpeed(float moveSpeed)
    {
        base.UpdateMovementSpeed(moveSpeed);
    }
}
