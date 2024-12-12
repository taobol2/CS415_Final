using System.Collections;
using UnityEngine;

public class Player : Entity  
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;
    public bool isBusy {  get; private set; }
    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpForce = 12f;

    [Header("Dash Info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir {  get; private set; } 


    #region States
    public PlayerStateMachine stateMachine { get; private set; }


    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }

    public PlayerWallSlideState wallSlideState { get; private set; }

    public PlayerWallJumpState wallJumpState { get; private set; }

    public PlayerAttackState attackState { get; private set; }  

    public PlayerCounterAttackState counterAttackState { get; private set; }
    #endregion

    protected override void Awake()
    {

        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
     }

    protected override void Start() { 

       base.Start();

        stateMachine.Initialized(idleState);


       
    }

    protected override void Update()
    {

        base.Update();
        stateMachine.currentState.Update();
        CheckDashInput();

    }

    public IEnumerator BusyFor(float _sec) { 
    
        isBusy = true;
        yield return new WaitForSeconds(_sec);
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckDashInput() {

        if (IsWallDetected()) {
            return;
        }

        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0 ) {

            dashUsageTimer = dashCooldown;

            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0) { 
                dashDir = facingDir;
            }
            stateMachine.ChangeState(dashState);
        }
    }


   
}

