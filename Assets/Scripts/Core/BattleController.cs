using System.Collections;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private readonly WaitForSeconds Interval = new(1.2f);

    private Coroutine _coroutine;

    public void ApplyBuff(int[] result)
    {
        Release();
        _coroutine = StartCoroutine(BuffProcess(result));
    }

    private IEnumerator BuffProcess(int[] result)
    {
        foreach (int slot in result)
        {
            switch (slot)
            {
                case 0:
                    Game.Wallet.Add(2);
                    break;

                case 1:
                    Game.Locator.Player.Heal();
                    yield return Interval;
                    break;

                case 2:
                    Game.Locator.Player.ApplyPercentDefence();
                    yield return Interval;
                    break;

                case 3:
                    Game.Locator.Player.ApplyPercentDamage();
                    yield return Interval;
                    break;

                case 4:
                    if (Game.Locator.Player.IsDoubleDamage) break;
                    Game.Locator.Player.IsDoubleDamage = true;
                    yield return Interval;
                    break;
            }
            yield return null;
        }

        StartBattleProcess();
    }

    private void StartBattleProcess()
    {
        Release();
        _coroutine = StartCoroutine(BattleProcess());
    }

    private IEnumerator BattleProcess()
    {
        Game.Locator.SkinView.Play(0);
        Game.Audio.PlayClip(0);
        yield return new WaitForSeconds(1f);

        Game.Locator.Player.ApplyDamage();
        Game.Locator.SkinView.Play(1);

        if (Game.Locator.Enemy.Health <= 0) yield break;

        yield return Interval;

        switch (Random.Range(0, 3))
        {
            case 0:
                Game.Locator.Enemy.Heal();
                break;

            case 1:
                Game.Locator.Enemy.ApplyPercentDefence();
                break;

            case 2:
                Game.Locator.Enemy.ApplyPercentDamage();
                break;

            case 3:
                Game.Locator.Enemy.IsDoubleDamage = true;
                break;
        }

        yield return Interval;
        Game.Audio.PlayClip(1);
        Game.Locator.Enemy.ApplyDamage();

        if (Game.Locator.Player.Health <= 0) yield break;


        yield return Interval;
        Game.Locator.AttackButton.SetActive(true);
        Game.Locator.PauseButton.SetActive(true);
    }

    public void Release()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}