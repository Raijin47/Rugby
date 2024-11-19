using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _skinsBoss;
    [SerializeField] private Sprite[] _skinsEnemies;

    public override void Ressurection()
    {
        _spriteRenderer.sprite = Game.Locator.Stats.Stage % 5 == 0 ? _skinsBoss[Random.Range(0, _skinsBoss.Length)] : _skinsEnemies[Random.Range(0, _skinsEnemies.Length)];
        base.Ressurection();
    }
    public override void ApplyDamage() 
    { 
        Game.Locator.Player.TakeDamage(IsDoubleDamage ? _stats.Attack * 2 : _stats.Attack);
        IsDoubleDamage = false;
    }
    protected override void Release() => Game.Action.SendWin();
}