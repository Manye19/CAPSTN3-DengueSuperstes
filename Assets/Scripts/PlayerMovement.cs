using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float currentSpeed;
    private bool facingRight = true;
    private Vector2 movement;
    public Rigidbody2D rb;

    private void Start()
    {
        // Set Player Movement Speed
        currentSpeed = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().movementSpeed;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (currentSpeed * Time.fixedDeltaTime));

        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        if (movement.x < 0 && facingRight)
        {
            Flip();
        }
    }
    
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
    
    public void UpdateMovementSpeed(float speed)
    {
        // Updates speed when Player hits an upgrade
        currentSpeed = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().movementSpeed;
    }
}
