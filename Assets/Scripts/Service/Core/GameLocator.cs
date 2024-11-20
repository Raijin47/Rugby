using System;
using UnityEngine;

[Serializable]
public class GameLocator
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private BattleStats _stats;
    [SerializeField] private BattleController _controller;
    [SerializeField] private SkinView _skinView;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _attackButton;
    [SerializeField] private GameObject _panelUpgrade;
    [SerializeField] private GameObject _textStage;

    public Player Player => _player;
    public Enemy Enemy => _enemy;
    public BattleStats Stats => _stats;
    public BattleController Controller => _controller;
    public SkinView SkinView => _skinView;
    public GameObject PauseButton => _pauseButton;
    public GameObject AttackButton => _attackButton;
    public GameObject PanelUpgrade => _panelUpgrade;
    public GameObject TextStage => _textStage;
}