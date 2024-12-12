using UnityEngine;

public class PlayerMono : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xInput;
    private int facingDir = 1;
    private bool facingRight = true;
    private bool isGrounded;
    private Animator anim;

    [SerializeField] private float v;
    [SerializeField] private float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashV;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDis;
    [SerializeField] private LayerMask whatGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        Movement();
        CheckInput();
        CollisionChecks();

        dashTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            dashTime = dashDuration;
        }

        AnimatorControllers();
        FlipCotroller();

    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDis, whatGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
        if (dashTime > 0)
        {
            rb.linearVelocity = new Vector2(xInput * dashV, 0);
        }
        else {
            rb.linearVelocity = new Vector2(xInput * v, rb.linearVelocity.y);
        }
        
    }

    private void Jump()
    {
        if (isGrounded) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.linearVelocity.x != 0;

        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);
    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipCotroller() {
        if (rb.linearVelocity.x > 0 && !facingRight) {
            Flip();
        } else if (rb.linearVelocity.x < 0 && facingRight) {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDis));
    }
}
