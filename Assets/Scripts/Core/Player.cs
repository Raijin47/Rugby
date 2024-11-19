public class Player : Character
{
    protected override void Release() => Game.Action.SendGameOver();
    public override void ApplyDamage() 
    {
        Game.Locator.Enemy.TakeDamage(IsDoubleDamage ? _stats.Attack * 2 : _stats.Attack);
        IsDoubleDamage = false;
    } 

    public void IncreaseAttack()
    {
        _stats.Attack += 10;
    }
    public void IncreaseHealth()
    {
        _stats.MaxHealth += 30;
    }
    public void IncreaseDefence()
    {
        _stats.Defence += 5;
    }
}