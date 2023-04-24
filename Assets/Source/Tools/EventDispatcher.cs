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

    public static void EnemyKilled(int rowIndex, int columnIndex)
    {
        OnEnemyKilled(rowIndex, columnIndex);
    }

    public static void EnemyReset()
    {
        OnEnemyReset();
    }

    public static void EnemyReachedEdge(bool goRight)
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
}
