using TMPro;
using UnityEngine;

public class BattleStats : MonoBehaviour
{
    [SerializeField] private ButtonBase _buttonAttack;
    [SerializeField] private GameObject _panelSlot;
    [SerializeField] private GameObject _panelWin;
    [SerializeField] private GameObject _panelUpgrade;
    [SerializeField] private GameObject _panelLose;

    [SerializeField] private TextMeshProUGUI[] _resultValueText;


    private int _stage;
    private readonly float Multiply = 1.15f;

    public int Stage
    {
        get => _stage;
        set
        {
            _stage = value;
            foreach(TextMeshProUGUI text in _resultValueText)
                text.text = $"{_stage}";

            Game.Locator.Enemy.InitStats(GetValue(50), GetValue(10), GetValue(5));
        }
    }

    private void Start()
    {
        Game.Action.OnWin.AddListener(Win);
        Game.Action.OnStart.AddListener(StartGame);
        Game.Action.OnLose.AddListener(Lose);
    }

    private void StartGame()
    {
        Stage = 1;
        Game.Locator.Player.InitStats(60, 10, 5);
    }

    public void Fight()
    {
        _panelSlot.SetActive(false);
        Game.Locator.Player.ApplyDamage();

        if (Game.Locator.Enemy.Health <= 0) return;
        Game.Locator.Enemy.ApplyDamage();

        if (Game.Locator.Player.Health <= 0) return;

        _buttonAttack.gameObject.SetActive(true);
    }

    private void Win()
    {
        _panelUpgrade.SetActive(Stage % 5 != 0);
        _panelWin.SetActive(Stage % 5 == 0);
    }

    public void NextStage() => Stage++;

    private void Lose()
    {
        _panelLose.SetActive(true);
    }

    private int GetValue(int value)
    {
        float random = Random.Range(0.85f, 1.15f);
        float baseValue = value;
        float stage = _stage;

        float result = baseValue * random * Mathf.Pow(Multiply, stage);

        return _stage == 5 ? (int)(result * 2) : _stage == 10 ? (int)(result * 5) : (int)result;
    }
}