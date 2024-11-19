using TMPro;
using UnityEngine;

public class BattleStats : MonoBehaviour
{
    [SerializeField] private ButtonBase _buttonAttack;
    [SerializeField] private GameObject _panelWin;
    [SerializeField] private GameObject _panelUpgrade;

    [SerializeField] private TextMeshProUGUI[] _resultValueText;
    [SerializeField] private TextMeshProUGUI _stageText;


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
            Game.Locator.Enemy.Ressurection();
            _stageText.text = $"STAGE {_stage}";
        }
    }

    private void Start()
    {
        Game.Action.OnWin.AddListener(Win);
        Game.Action.OnStart.AddListener(StartGame);
    }

    private void StartGame()
    {
        Stage = 1;
        Game.Locator.Player.InitStats(70, 10, 5);
        _buttonAttack.gameObject.SetActive(true);
    }

    private void Win()
    {
        _panelUpgrade.SetActive(Stage % 5 != 0);
        _panelWin.SetActive(Stage % 5 == 0);
        Game.Wallet.Add(Stage % 5 == 0 ? 30 : 5);
    }

    public void NextStage() => Stage++;

    private int GetValue(int value)
    {
        float random = Random.Range(0.85f, 1.15f);
        float baseValue = value;
        float stage = _stage;

        float result = baseValue * random * Mathf.Pow(Multiply, stage);

        return _stage == 5 ? (int)(result * 2) : _stage == 10 ? (int)(result * 3) : (int)result;
    }
}