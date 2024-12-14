using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    
    public Stat maxHP;

    public Stat damage;
    public Stat strength;
    

    
    [SerializeField] public int currentHealth;

    public System.Action onHealthChanged;
    protected virtual void Start()
    {
        currentHealth = GetMaxHP();
        
        
    }

    public virtual void DoDamage(CharacterStats _targetStats) { 
        int totalDmg = damage.GetValue() * strength.GetValue();
        _targetStats.TakeDmg(totalDmg);
    }

    public virtual void TakeDmg(int _damage)
    {

        DecreaseHealthBy(_damage);
        // currHP -= _damage;


        if (currentHealth <= 0) {
            Die();
        }

        // onHealthChanged();

    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;

        if (onHealthChanged != null) {
            onHealthChanged();
        }
    }

    protected virtual void Die()
    {
        
    }

    public int GetMaxHP()
    {
        return maxHP.GetValue();
    }
}
