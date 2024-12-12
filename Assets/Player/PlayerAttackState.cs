using UnityEngine;

public class PlayerAttackState : PlayerState
{

    private int comboCounter;
    private float lastTimerAttacked;
    private float comboWindow = 2;
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;
        if (comboCounter > 2 || Time.time >= lastTimerAttacked + comboWindow) { 
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter",comboCounter);

        float attackDir = player.facingDir;
        if (xInput != 0) {
            attackDir = xInput;

        }

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f);
        comboCounter++;
        lastTimerAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            rb.linearVelocity = new Vector2(0, 0);

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
  
