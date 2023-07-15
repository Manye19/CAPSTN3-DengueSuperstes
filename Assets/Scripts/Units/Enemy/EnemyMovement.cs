using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    protected float currentSpeed;
    protected EnemyStat enemyStat;
    private GameObject player;
    protected Vector2 targetPos;
    private Coroutine moveToPlayerCo;
    [SerializeField] private bool isFacingRight = false;

    [Header(DS_Constants.ASSIGNABLE)] 
    [SerializeField] private float updateTick;

    protected virtual void OnEnable()
    {
        player = SingletonManager.Get<GameManager>().player;
        moveToPlayerCo = StartCoroutine(UpdatePlayerLocation());
    }

    protected virtual void Start()
    {
        targetPos = player.transform.position;
        enemyStat = GetComponent<EnemyStat>();
        currentSpeed = enemyStat.statSO.moveSpeed;
    }
    protected virtual void FixedUpdate()
    {
        // Get and set UPDATED speed (int) from EnemyStat class or Spawner class
        transform.position = Vector2.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime);

        if (transform.position.x > targetPos.x)
        {
            Flip();
        }
        else if (transform.position.x < targetPos.x)
        {
            Flip();
        }
    }

    protected virtual IEnumerator UpdatePlayerLocation()
    {
        while (true)
        {
            targetPos = player.transform.position;
            yield return new WaitForSeconds(updateTick);
        }
    }

    public virtual void AddListenerToChangeTarget()
    {
        SingletonManager.Get<GameManager>().onChangeTargetEvent.AddListener(ChangeTarget);
    }

    public virtual void RemoveListenerToChangeTarget()
    {
        SingletonManager.Get<GameManager>().onChangeTargetEvent.RemoveListener(ChangeTarget);
    }

    public virtual void ChangeTarget(Transform target)
    {
        if (target != null)
        {
            StopCoroutine(moveToPlayerCo);
            targetPos = target.position;
        }        
        else if (isActiveAndEnabled)
        {
            moveToPlayerCo = StartCoroutine(UpdatePlayerLocation());
        }
    }

    public virtual void UpdateMovementSpeed(float moveSpeed)
    {
        // Updates speed depending on Enemy?
        currentSpeed = moveSpeed;
    }
    
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }
}
