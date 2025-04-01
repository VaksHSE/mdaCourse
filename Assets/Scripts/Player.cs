using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D col;
    private float xInput;
    private float yInput;


    
    [SerializeField] private int coins;
    [SerializeField] private float speed = 120.0f;
    [SerializeField] GameObject deadVFX;

    [Header("Move")]
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float doubleJumpForce = 5;

    [SerializeField] private bool isJumping = true;
    [SerializeField] private bool canDoubleJump = true;
    private bool isWallJumping = false;

    [Header("Wall")]
    [SerializeField] private Vector2 wallJumpForce;
    [SerializeField] private float wallJumpDur = 1;

    [Header("Buff")]
    [SerializeField] private float bufferJumpWindow = 0.25f;
    private float bufferJumpPressed = -2.0f;

    [Header("Coyote")]
    [SerializeField] private float coyoteJumpWindow = 0.25f;
    private float coyoteJumpActivated = -2.0f;

    private bool canBeControlled = false;
    private float defauldGravityScale;

    [Header("Checker")]
    [SerializeField] private float groundCheckDist;
    [SerializeField] private float wallCheckDist;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool isGroundDetected = true;
    [SerializeField] private bool isWallDetected = true;
    [Space]
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;
    [SerializeField] private LayerMask whatIsEnemy;

    [Header("Knockback")]
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private Vector2 knockbackPower;
    private bool isKnocked;

    private int facingDir = 1;
    private bool facingRight = true;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        if(Cinema.instance)
        Cinema.instance.linkCinema(this.gameObject.transform);
    }
    void Start()
    {
        defauldGravityScale = rb.gravityScale;
        RespawnFinished(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.instance.OffSound();
        }
        //Debug.Log(rb.velocity.y);
        Land();
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Knockback(100);
        //}
        if (canBeControlled == false)
        {
            Collision();
            Animations();
            return;
        }
        if (isKnocked) // po idee zamenit ma canBeControlled
            return;
        HandleEnemyDetection();
        WallSlide();
        Movement();
        FlipMove();
        Collision();
        Animations();
    }
    private void WallSlide()
    {
        bool canWallSlide = isWallDetected && rb.linearVelocity.y < 0;
        float yModifer = yInput < 0 ? 1 : .05f;
        if (canWallSlide == false)
            return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * yModifer);
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpBottom();
            if (isJumping)
                bufferJumpPressed = Time.time;
        }

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        if (isWallDetected)
            return;
        if (isWallJumping)
            return;
        rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocity.y);

    }
    void JumpBottom()
    {
        bool coyoteJumpAvalable = Time.time < coyoteJumpActivated + coyoteJumpWindow;
        //Debug.Log(coyoteJumpAvalable);
        if (isGroundDetected || coyoteJumpAvalable)
        {
            if (coyoteJumpAvalable)
                Debug.Log("Coyote activeted");
            canDoubleJump = true;
            Jump();
        }
        else if (isWallDetected && !isGroundDetected)
        {
            WallJump();
        }
        else if (canDoubleJump)
        {
            //Debug.Log("double");
            DoubleJump();
        }
        CancelCoyoteJump();
    }
    void WallJump()
    {
        canDoubleJump = true;
        rb.linearVelocity = new Vector2(wallJumpForce.x * -facingDir, wallJumpForce.y);
        Flip();
        StopAllCoroutines();
        StartCoroutine(WallJumpCoroutine());
    }
    private IEnumerator WallJumpCoroutine()
    {
        isWallJumping = true;
        yield return new WaitForSeconds(wallJumpDur);
        isWallJumping = false;

    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    void DoubleJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, doubleJumpForce);
        canDoubleJump = false;
    }

    void Collision()
    {
        isGroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDist, whatIsGround); // facingDir - ymno
    }

    void Land()
    {
        //Debug.Log("isJumping");
        //Debug.Log(isJumping);
        //Debug.Log("isGroundDetected");
        //Debug.Log(isGroundDetected);
        if (isGroundDetected && isJumping)
        {
            isJumping = false;
            canDoubleJump = true;
            AttemptBufferJump();
        }
        
        if (!isGroundDetected && !isJumping)
        {
            isJumping = true;
            if (rb.linearVelocity.y < 0)
            {
                Debug.Log("Coyote");
                ActivateCoyoteJump();
            }
            else
            {
                Debug.Log("!!!!!!!!!!!!!!!!!!");
                Debug.Log(rb.linearVelocity.y);
            }
        }


    }
    private void ActivateCoyoteJump()
    {
        coyoteJumpActivated = Time.time;
    }
    private void CancelCoyoteJump()
    {
        coyoteJumpActivated = Time.time - 1;
    }
    void AttemptBufferJump()
    {
        //Debug.Log("mda");
        if (Time.time < bufferJumpWindow + bufferJumpPressed)
        {
            bufferJumpPressed = Time.time - 1;
            Jump();
        }
    }
    void FlipMove()
    {
        if (xInput < 0 && facingRight || (xInput > 0 && !facingRight))  // tyt xInput a ne rb.velocity.x ibo sverxy isWallDetected ret  
        {
            Flip();
        }
    }
    void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }



    void Animations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGround", isGroundDetected);
        anim.SetBool("isWallDetected", isWallDetected);
    }

    public void Knockback(float sourceDamageXPosition)
    {
        AudioManager.instance.PlaySound(1);
        float knockbackDir = -1;
        if (transform.position.x < sourceDamageXPosition)
        {
            knockbackDir = 1;
        }
        if (isKnocked)
            return;
        StartCoroutine(KnockbackRoutine());
        //anim.SetTrigger("knock");
        //rb.velocity = new Vector2(knockbackPower.x * -facingDir, knockbackPower.y);
        rb.linearVelocity = new Vector2(knockbackPower.x * -knockbackDir, knockbackPower.y);
    }
    private IEnumerator KnockbackRoutine()
    {
        //canBeKnocked = false;
        isKnocked = true;
        anim.SetBool("knocked", true);
        yield return new WaitForSeconds(knockbackDuration);

        //canBeKnocked = true;
        isKnocked = false;
        anim.SetBool("knocked", false);

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDist));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + wallCheckDist * facingDir, transform.position.y));

        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
    }

    public void Die()
    {
        AudioManager.instance.PlaySound(1);

        GameObject newDeadVFX = Instantiate(deadVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public void RespawnFinished(bool finished)
    {
        if (finished)
        {
            rb.gravityScale = defauldGravityScale;
            canBeControlled = true;
            col.enabled = true;
        }
        else
        {
            canBeControlled = false;
            rb.gravityScale = 0;
            col.enabled = false;
        }
    }




    private void HandleEnemyDetection()
    {
        if (rb.linearVelocity.y >= 0)
            return;
        Debug.Log("ZZZ");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius, whatIsEnemy);
        foreach (var enemy in colliders)
        {
            Enemy newEnemy = enemy.GetComponent<Enemy>();
            if (newEnemy != null)
            {
                Debug.Log("1");
                newEnemy.Die();
                Jump();
            }
        }
    }
}

