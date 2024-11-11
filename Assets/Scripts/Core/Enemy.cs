public class Enemy : Character
{
    protected override Character Target => Game.Locator.Player;

    protected override void Release() => Game.Action.SendWin();
}