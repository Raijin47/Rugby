using System;
using UnityEngine.Events;

[Serializable]
public class GameAction
{
    public UnityEvent OnStart;
    public UnityEvent OnLose;
    public UnityEvent OnWin;
    public UnityEvent OnRestart;

    public void SendStartGame() => OnStart?.Invoke();
    public void SendGameOver() => OnLose?.Invoke();
    public void SendWin() => OnWin?.Invoke();
    public void SendRestart() => OnRestart?.Invoke();
}