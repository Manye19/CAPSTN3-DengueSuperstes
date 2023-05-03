using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float currentSpeed;
    private bool isFacingRight = true;
    
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set Player Movement Speed
        currentSpeed = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().movementSpeed;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        /*if (rb.velocity.x != 0)
        {
            if (rb.velocity.x > 0)
            {
                Debug.Log("Player is moving right");
            }
            else
            {
                Debug.Log("Player is moving left");
            }
        }
        if (rb.velocity.y != 0)
        {
            if (rb.velocity.y > 0)
            {
                Debug.Log("Player is moving up");
            }
            else
            {
                Debug.Log("Player is moving down");
            }
        }*/
    }

    private void FixedUpdate()
    {
        // Changed Player Movement to Force
        //rb.MovePosition(rb.position + movement * (currentSpeed * Time.fixedDeltaTime));
       
        // Player Movement is now through force; experimenting...
        Vector2 movement = new Vector2(this.movement.x, this.movement.y).normalized;
        rb.AddForce(movement * currentSpeed);

        if (movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
        if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }
    }
    
    #region Functions
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }
    
    public void UpdateMovementSpeed(float speed)
    {
        // Updates speed when Player hits an upgrade
        currentSpeed = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().movementSpeed;
    }
    #endregion
}
