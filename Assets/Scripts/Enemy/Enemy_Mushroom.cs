using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy
{
    
    protected override void Update()
    {
        base.Update();
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        if (isDead)
            return;
        HandleCollision();
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
            idleTimer = idleDuration;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if (idleTimer > 0)
        {
            return;
        }
        //Debug.Log(isGrounded);
        if (isGrounded) {
            //Debug.Log("yasosal");
            rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocity.y);
        }
    }

    
}
