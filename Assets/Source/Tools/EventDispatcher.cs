using UnityEngine;
using System;

/// <summary>
/// Registers events and informs delegates whene those are triggered.
/// </summary>
public class EventDispatcher : MonoBehaviour
{

    public static Action<int, int, bool, float> OnEnemyKilled = delegate { };
    public static Action OnEnemyReset = delegate { };
    public static Action<bool> OnEnemyReachEdge = delegate { };
    public static Action<int> OnScoreGained = delegate { };
    public static Action<bool> OnPauseMenuOpen = delegate { };
    public static Action<float> OnPlayerShot = delegate { };
    public static Action<float> OnEnemyShot = delegate { };
    public static Action<float> OnPlayerHit = delegate { };
    public static Action<float> OnPlayerKilled = delegate { };

    public static void EnemyKilled(int rowIndex, int columnIndex, bool autoKill, float xCoord)
    {
        OnEnemyKilled(rowIndex, columnIndex, autoKill, xCoord);
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

    public static void PlayerShot(float xCoord)
    {
        OnPlayerShot(xCoord);
    }

    public static void EnemyShot(float xCoord)
    {
        OnEnemyShot(xCoord);
    }

    public static void PlayerHit(float xCoord)
    {
        OnPlayerHit(xCoord);
    }

    public static void PlayerKilled(float xCoord)
    {
        OnPlayerKilled(xCoord);
    }
}
