using TMPro;
using UnityEngine;

public abstract class ShopElement : MonoBehaviour
{
    [SerializeField] protected int _id;
    [SerializeField] private ButtonBase _buttonEquip;
    [SerializeField] private ButtonBase _buttonBuy;
    [SerializeField] private TextMeshProUGUI _textPrice;
    [SerializeField] private TextMeshProUGUI _textState;
    [SerializeField] private GameObject _enable;
    [SerializeField] private GameObject _disable;

    [SerializeField] private int _price = 400;

    protected abstract string SaveName { get; }
    public abstract bool IsEquip { get; set; }

    public bool IsPurchased
    {
        get => _id == 0 || PlayerPrefs.GetInt(SaveName + _id, 0) == 1;
        set 
        { 
            PlayerPrefs.SetInt(SaveName + _id, value ? 1 : 0);
            _enable.SetActive(value);
            _disable.SetActive(!value);
        } 
    }

    protected virtual void Start()
    {
        _buttonBuy.OnClick.AddListener(Buy);
        _buttonEquip.OnClick.AddListener(() => IsEquip = true);
        _textPrice.text = _price.ToString();
        IsPurchased = IsPurchased;
        UpdateUI();
    }

    protected void UpdateUI()
    {
        _textState.text = IsEquip ? "Equipped" : "Equip";
        _buttonEquip.Interactable = !IsEquip;
    }

    private void Buy()
    {
        if (Game.Wallet.Spend(_price))
        {
            IsPurchased = true;
            IsEquip = true;
        }
    }
}