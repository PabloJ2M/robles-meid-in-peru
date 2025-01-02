using System;
using UnityEngine;

[DefaultExecutionOrder(-500)]
public class GameManager : SimpleSingleton<GameManager>
{
    public event Action onRestart;
    public event Action onContinue;

    public bool isRunning, isInMenu = true;

    public void Restart() { onRestart?.Invoke(); Continue(); }
    public void Continue() => onContinue?.Invoke();
    public void SetGameState(bool value) => isRunning = isInMenu = value;
}