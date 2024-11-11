using System;
using UnityEngine;

[Serializable]
public class GameLocator
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private BattleStats _battle;

    public Player Player => _player;
    public Enemy Enemy => _enemy;
    public BattleStats Battle => _battle;
}