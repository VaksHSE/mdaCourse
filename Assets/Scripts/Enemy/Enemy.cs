using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    protected bool canMove = true;

    [SerializeField] protected GameObject damageTrigger;
    [Space]
    [Header("Basic collision")]
    [SerializeField] protected float groungCheckDist = 1.1f;
    [SerializeField] protected float wallCheckDist = 0.7f;
    [SerializeField] protected float idleDuration = 0f;
    [SerializeField] protected float idleTimer = 0f;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask whatIsPlayer;


    [SerializeField] protected Transform player;

    [Header("Death details")]
    [SerializeField] private float deathImpact;
    [SerializeField] private float deathRotationSpeed;
    private int deathRotationDir = 1;
    protected bool isDead = false;

    protected bool isGrounded;

    protected bool isGroundednInFrontDetected;
    protected bool isWallDetected;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D col;

    protected int facingDir = -1;
    protected bool facingRight = false;
    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    private void Start()
    {
        //player = GameManager.Instance.player.transform;
        //Debug.Log(GameManager.Instance.player);
        InvokeRepeating(nameof(UpdatePlayerRef), 0, 2);
    }
    private void UpdatePlayerRef()
    {
        Debug.Log(GameManager.Instance);
        if (player == null)
        {
            if (GameManager.Instance.player)
            player = GameManager.Instance.player.transform;
        }
    }

    protected virtual void HandleFlip(float xValue)
    {
        if (xValue < transform.position.x && facingRight || (xValue > transform.position.x && !facingRight))  // tyt xInput a ne rb.velocity.x ibo sverxy isWallDetected ret  
        {
            Flip();
        }
    }
    protected virtual void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }
    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groungCheckDist, whatIsGround);
        isGroundednInFrontDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groungCheckDist, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDist, whatIsGround); // facingDir - ymno
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groungCheckDist));
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groungCheckDist));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + wallCheckDist * facingDir, transform.position.y));
    }

    protected virtual void Update()
    {
        idleTimer -= Time.deltaTime;
        if (isDead)
            HandleDeath();
    }

    public virtual void Die()
    {
        AudioManager.instance.PlaySound(1);
        col.enabled = false;
        damageTrigger.SetActive(false);
        anim.SetTrigger("hit");
        isDead = true;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, deathImpact);
        if (Random.Range(0, 10) < 50)
        {
            deathRotationDir *= -1;
        }

        Invoke(nameof(DestroyMe), 1);
    }
    private void HandleDeath()
    {
        transform.Rotate(0, 0, deathRotationSpeed * Time.deltaTime * deathRotationDir);
    }

    protected virtual void DestroyMe()
    {
        Destroy(gameObject);          //my
    }
}
