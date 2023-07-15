using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private float currentSpeed;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private Vector2 movement;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set Player Movement Speed
        currentSpeed = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().statSO.moveSpeed;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Changed Player Movement to Force
        //rb.MovePosition(rb.position + movement * (currentSpeed * Time.fixedDeltaTime));
       
        // Player Movement is now through force; experimenting...
        Vector2 movement = new Vector2(this.movement.x, this.movement.y).normalized;
        rb.MovePosition(rb.position + movement * (currentSpeed * Time.deltaTime));

        //Debug.Log(rb.velocity.normalized);
        
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
        currentSpeed = speed;
    }
    #endregion
}
