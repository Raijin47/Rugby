using UnityEngine;

public class SpinController : MonoBehaviour
{
    public static SpinController Instance;

    [SerializeField] private GameObject _panelSlot;
    [SerializeField] private GameObject _buttonAttack;
    [SerializeField] private Slot[] _slots;

    [Space(10)]
    [SerializeField] private float _offsetVertical;
    [SerializeField] private float _offsetHorizontal;

    [Space(10)]
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _endSpeed;
    [SerializeField] private float _minSpeed;

    [SerializeField] private float _timeSpin;

    public float EndSpeed => _endSpeed;
    public float MinSpeed => _minSpeed;
    public float TimeSpin => _timeSpin;
    public float Offset => _offsetVertical;

    private void Awake() => Instance = this;

    private void OnEnable()
    {
        foreach (Slot slot in _slots) slot.SetPosition(_offsetVertical);
    }

    public void Spin()
    {
        _panelSlot.SetActive(true);
        _buttonAttack.SetActive(false);

        float speed = _startSpeed * (Random.value + 1);
        float increaseSpeed = Random.value + 1;

        for(int i = 0; i < _slots.Length; i++)
        {
            _slots[i].Spin(speed);
            speed *= increaseSpeed;
        }
    }

    public void GetResult()
    {
        bool isActive = true;

        foreach (Slot slot in _slots)
            if (slot.IsActive) isActive = false;

        if (!isActive) return;

        int[] result = new[] { _slots[0].Result, _slots[1].Result, _slots[2].Result };

        Game.Locator.Controller.ApplyBuff(result);

        _panelSlot.SetActive(false);
    }

    private void OnValidate()
    {
        _slots = gameObject.GetComponentsInChildren<Slot>();

        float totalHeight = _slots.Length * _offsetHorizontal;

        for (int i = 0; i < _slots.Length; i++)
        {
            float pos = -totalHeight / 2 + i * _offsetHorizontal + _offsetHorizontal / 2;
            if (Mathf.Abs(pos) < 0.1f) pos = 0;

            RectTransform transform = _slots[i].GetComponent<RectTransform>();
            transform.anchoredPosition = new Vector2(pos, 0);
        }

        foreach (Slot slot in _slots) slot.SetPosition(_offsetVertical);       
    }
}