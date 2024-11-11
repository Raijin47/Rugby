using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected Stats _stats;
    protected abstract Character Target { get; }
    public int Health => _stats.Health;

    public void InitStats(int health, int attack, int defence)
    {
        _stats.MaxHealth = health;
        _stats.Health = health;
        _stats.Attack = attack;
        _stats.Defence = defence;
    }

    public void ApplyDamage() => Target.TakeDamage(_stats.Attack);

    public void TakeDamage(int value)
    {
        int damage = value;
        if(_stats.Defence >= damage) return;
        damage -= _stats.Defence;

        _stats.Health -= damage;
        if (_stats.Health <= 0)
        {
            _stats.Health = 0;
            Release();
        }
    }

    protected abstract void Release();
}