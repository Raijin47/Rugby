using UnityEngine;

public class Player : Character
{
    protected override Character Target => Game.Locator.Enemy;

    protected override void Release() => Game.Action.SendGameOver();

    public void ApplyDoubleDamage()
    {
        _stats.Attack *= 2;
    }

    public void ApplyPercentDamage()
    {
        float current = _stats.Attack;
        _stats.Attack = Mathf.RoundToInt(current * 1.1f);
    }

    public void ApplyPercentDefence()
    {
        float current = _stats.Defence;
        _stats.Defence = Mathf.RoundToInt(current * 1.1f);
    }

    public void Heal()
    {
        float value = _stats.MaxHealth;
        _stats.Health += (int)(value * 0.03f);
        if (_stats.Health >= _stats.MaxHealth) _stats.Health = _stats.MaxHealth;
    }

    public void IncreaseAttack()
    {
        _stats.Attack += 10;
    }
    public void IncreaseHealth()
    {
        _stats.MaxHealth += 10;
    }
    public void IncreaseDefence()
    {
        _stats.Defence += 5;
    }
}