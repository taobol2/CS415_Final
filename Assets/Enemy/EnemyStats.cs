using UnityEngine;

public class EnemyStats : CharacterStats
{

    private Enemy enemy;
    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    public override void TakeDmg(int damage)
    {
        base.TakeDmg(damage);

        enemy.DamageEffect();
    }

    protected override void Die()
    {

        base.Die();
        enemy.Die();
    }
}
