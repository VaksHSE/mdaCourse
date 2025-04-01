using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemuChicken : Enemy
{

    [Header("Dino")]
    [SerializeField] private float aggroDur;
    private float aggroTimer;
    [SerializeField] private bool playerDetected;
    [SerializeField] private float detectionRadius;

    protected override void Update()
    {
        base.Update();
        anim.SetFloat("xVelocity", rb.linearVelocityX);
        aggroTimer -= Time.deltaTime;
        if (isDead)
            return;

        HandleCollision();
        if (playerDetected)
        {
            canMove = true;
            aggroTimer = aggroDur;
        }
        if (aggroTimer < 0)
        {
            canMove = false;
        }
        HandleMovement();
        //Debug.Log(isGroundedDetected);
        if (isGrounded)
            HandleTurnAround();

    }

    private void HandleTurnAround()
    {
        if (!isGroundednInFrontDetected || isWallDetected)
        {

            Flip();
            canMove = false;
            //idleTimer = idleDuration;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if (!canMove)
        {
            return;
        }
        Debug.Log(isGrounded);
        if (player)
            HandleFlip(player.position.x);
        if (isGrounded)
        {
            Debug.Log("yasosal");
            rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocity.y);
        }
    }
    protected override void HandleCollision()
    {
        base.HandleCollision();

        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, detectionRadius, whatIsPlayer);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + detectionRadius * facingDir, transform.position.y));

    }
}
