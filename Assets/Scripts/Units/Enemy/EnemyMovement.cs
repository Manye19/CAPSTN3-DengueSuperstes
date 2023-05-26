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

    [Header(DS_Constants.ASSIGNABLE)] 
    [SerializeField] private float timeTick;

    private void OnEnable()
    {
        player = SingletonManager.Get<GameManager>().player;
        targetPos = player.transform.position;
        currentSpeed = GetComponent<EnemyStat>().movementSpeed;
        StartCoroutine(UpdatePlayerLocation());
    }

    private void Update()
    {
        // Get and set UPDATED speed (int) from EnemyStat class or Spawner class
        transform.position = Vector2.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime); // temporary for now
    }

    private IEnumerator UpdatePlayerLocation()
    {
        while (true)
        {
            targetPos = player.transform.position;
            yield return new WaitForSeconds(timeTick);
        }
    }

    public void UpdateMovementSpeed(float speed)
    {
        // Updates speed depending on Enemy?
        currentSpeed = speed;
    }
}
