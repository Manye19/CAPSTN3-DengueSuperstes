using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed; // should be currentMovementSpeed or something

    private GameObject target;
    private float distance;

    private void OnEnable()
    {
        target = SingletonManager.Get<GameManager>().player;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        
        // Get and set UPDATED speed (int) from EnemyStat class or Spawner class
        transform.position =Vector2.MoveTowards(this.transform.position, target.transform.position, movementSpeed * Time.deltaTime); // temporary for now
    }
}
