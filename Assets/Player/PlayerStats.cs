using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void TakeDmg(int damage)
    {
        base.TakeDmg(damage);
        player.DamageEffect();
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
    }
}
