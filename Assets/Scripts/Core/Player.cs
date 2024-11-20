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
        NextStage();
    }
    public void IncreaseHealth()
    {
        _stats.MaxHealth += 30;
        NextStage();
    }
    public void IncreaseDefence()
    {
        _stats.Defence += 5;
        NextStage();
    }

    private void NextStage()
    {
        Game.Locator.PauseButton.SetActive(true);
        Game.Locator.AttackButton.SetActive(true);
        Game.Locator.PanelUpgrade.SetActive(false);
        Game.Locator.Stats.NextStage();
    }
}