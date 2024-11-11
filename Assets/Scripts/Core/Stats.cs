using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Stats
{
    [SerializeField] private TextMeshProUGUI _textAttack;
    [SerializeField] private TextMeshProUGUI _textDefence;
    [SerializeField] private TextMeshProUGUI _textHealth;
    [SerializeField] private Image _healthBar;

    private int _maxHealth;
    private int _health;
    private int _attack;
    private int _defence;

    public int MaxHealth
    {
        get => _maxHealth;
        set 
        {
            _maxHealth = value;
            UpdateHealthUI();
        } 
    }

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            UpdateHealthUI();
        }
    }

    public int Attack
    {
        get => _attack;
        set
        {
            _attack = value;
            _textAttack.text = _attack.ToString();
        }
    }

    public int Defence
    {
        get => _defence;
        set
        {
            _defence = value;
            _textDefence.text = _defence.ToString();
        }
    }

    private void UpdateHealthUI()
    {
        _textHealth.text = $"{_health}/{MaxHealth}";
        _healthBar.fillAmount = (float)Health / (float)MaxHealth;
    }
}