using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    private float currentSpeed;
    private GameObject player;
    private Vector2 targetPos;
    private Coroutine moveToPlayerCo;

    [Header(DS_Constants.ASSIGNABLE)] 
    [SerializeField] private float updateTick;

    private void Start()
    {
        // moveToPlayerCo; setting up for Mosquito Magnet
        moveToPlayerCo = StartCoroutine(UpdatePlayerLocation());
    }

    private void OnEnable()
    {
        player = SingletonManager.Get<GameManager>().player;
        targetPos = player.transform.position;
        currentSpeed = GetComponent<EnemyStat>().statSO.moveSpeed;
        
        // lags at 100+ entities because physics messes up when they collide at each other
        // StartCoroutine(Move()); 
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

    public void UpdateMovementSpeed(float moveSpeed)
    {
        // Updates speed depending on Enemy?
        currentSpeed = moveSpeed;
    }
}
