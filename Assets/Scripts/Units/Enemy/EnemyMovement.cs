using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    private float currentSpeed;
    private GameObject target;
    private float distance;

    private void OnEnable()
    {
        target = SingletonManager.Get<GameManager>().player;
        currentSpeed = GetComponent<EnemyStat>().movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        
        // Get and set UPDATED speed (int) from EnemyStat class or Spawner class
        transform.position =Vector2.MoveTowards(this.transform.position, target.transform.position, currentSpeed * Time.deltaTime); // temporary for now
    }

    public void UpdateMovementSpeed(float speed)
    {
        // Updates speed depending on Enemy?
        currentSpeed = speed;
    }
}
