using UnityEngine;

public class PlayerFallAttackState : PlayerState
{
    public PlayerFallAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, player.fallAttackV);    
       
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);
    }

    public override void Update()
    {
        base.Update();
        

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
