using UnityEngine;

public class SkeletonBattleState : EnemyState
{

    private EnemySkeleton enemy;
    private Transform player;
    private int movDir;
    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {

                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);

            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10) {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        

        if(player.position.x > enemy.transform.position.x)
            movDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            movDir= -1;

        enemy.SetVelocity(enemy.moveSpeed * movDir, rb.linearVelocity.y);
    }

    private bool CanAttack()
    {

        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown) { 
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
