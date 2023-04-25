using UnityEngine;
using System;

/// <summary>
/// Registers events and informs delegates whene those are triggered.
/// </summary>
public class EventDispatcher : MonoBehaviour
{

    public static Action<int, int> OnEnemyKilled = delegate { };
    public static Action OnEnemyReset = delegate { };
    public static Action<bool> OnEnemyReachEdge = delegate { };
    public static Action<int> OnScoreGained = delegate { };
    public static Action<bool> OnPauseMenuOpen = delegate { };
    public static Action OnPlayerShot = delegate { };
    public static Action OnEnemyShot = delegate { };
    public static Action OnPlayerHit = delegate { };
    public static Action OnPlayerKilled = delegate { };

    public static void EnemyKilled(int rowIndex, int columnIndex)
    {
        OnEnemyKilled(rowIndex, columnIndex);
    }

    public static void EnemyReset()
    {
        OnEnemyReset();
    }

    public static void EnemyReachedLateralEdge(bool goRight)
    {
        OnEnemyReachEdge(goRight);
    }

    public static void ScoreGained(int score)
    {
        OnScoreGained(score);
    }

    public static void PauseMenuOpen(bool open)
    {
        OnPauseMenuOpen(open);
    }

    public static void PlayerShot()
    {
        OnPlayerShot();
    }

    public static void EnemyShot()
    {
        OnEnemyShot();
    }

    public static void PlayerHit()
    {
        OnPlayerHit();
    }

    public static void PlayerKilled()
    {
        OnPlayerKilled();
    }
}
