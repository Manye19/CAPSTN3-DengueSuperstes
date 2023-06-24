using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    private float currentSpeed;
    private EnemyStat enemyStat;
    private GameObject player;
    private Vector2 targetPos;
    private Coroutine moveToPlayerCo;

    [Header(DS_Constants.ASSIGNABLE)] 
    [SerializeField] private float updateTick;

    private void Start()
    {
        player = SingletonManager.Get<GameManager>().player;
        targetPos = player.transform.position;
        enemyStat = GetComponent<EnemyStat>();
        currentSpeed = enemyStat.statSO.moveSpeed;
        moveToPlayerCo = StartCoroutine(UpdatePlayerLocation());
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameManager>().onChangeTargetEvent.RemoveListener(ChangeTarget);
    }

    private void FixedUpdate()
    {
        // Get and set UPDATED speed (int) from EnemyStat class or Spawner class
        transform.position = Vector2.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime); // temporary for now
    }

    private IEnumerator UpdatePlayerLocation()
    {
        while (true)
        {
            targetPos = player.transform.position;
            yield return new WaitForSeconds(updateTick);
        }
    }

    public void ListenToChangeTarget()
    {
        SingletonManager.Get<GameManager>().onChangeTargetEvent.AddListener(ChangeTarget);
    }

    private void ChangeTarget(Transform target)
    {
        if (target != null)
        {
            StopCoroutine(moveToPlayerCo);
            targetPos = target.position;
        }        
        else
        {
            moveToPlayerCo = StartCoroutine(UpdatePlayerLocation());
        }
    }

    public void UpdateMovementSpeed(float moveSpeed)
    {
        // Updates speed depending on Enemy?
        currentSpeed = moveSpeed;
    }
}
