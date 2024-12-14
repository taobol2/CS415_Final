using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public CharacterStats stats { get; private set; }

    public CapsuleCollider2D cd { get; private set; }
    #endregion

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;

    protected bool isKnocked;

    [Header("Collision Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGroud;

    public System.Action onFlipped;
    protected virtual void Awake() { 
    
    }

    
    protected virtual void Start() {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();
    }
    protected virtual void Update() {
    
    }

    public virtual void DamageEffect() {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
        
    }

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position,
        Vector2.down, groundCheckDistance, whatIsGroud);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position,
        Vector2.right * facingDir, wallCheckDistance, whatIsGroud);


    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,
            groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance,
            wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if(onFlipped != null) 
            onFlipped();
    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }

    public void SetVelocity(float _xV, float _yV)
    {
        if (isKnocked) { 
            return;
        }
        rb.linearVelocity = new Vector2(_xV, _yV);
        FlipController(_xV);
    }

    public void SetZeroV()
    {
        if (isKnocked)
            return;
        rb.linearVelocity = Vector2.zero;
    }


    protected virtual IEnumerator HitKnockback() {
        isKnocked = true;
        rb.linearVelocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    public virtual void Die()
    {

    }

}
