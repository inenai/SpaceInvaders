using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource playerShotSrc;
    [SerializeField] private AudioSource enemyShotSrc;
    [SerializeField] private AudioSource playerHitSrc;
    [SerializeField] private AudioSource playeKilledSrc;
    [SerializeField] private AudioSource enemyKilledSrc;

    private void Awake() {
        EventDispatcher.OnPlayerShot += PlayerShot;
        EventDispatcher.OnEnemyShot += EnemyShot;
        EventDispatcher.OnPlayerHit += PlayerHit;
        EventDispatcher.OnPlayerKilled += PlayerKilled;
        EventDispatcher.OnEnemyKilled += EnemyKilled;
    }

    private void PlayerShot() {
        playerShotSrc.Play();
    }

    private void EnemyShot()
    {
        enemyShotSrc.Play();
    }

    private void PlayerHit()
    {
        playerHitSrc.Play();
    }

    private void PlayerKilled()
    {
        playeKilledSrc.Play();
    }

    private void EnemyKilled(int rowIndex, int columnIndex, bool autoKill)
    {
        enemyKilledSrc.Play();
    }

    private void OnDestroy() {
        EventDispatcher.OnPlayerShot -= PlayerShot;
        EventDispatcher.OnEnemyShot -= EnemyShot;
        EventDispatcher.OnPlayerHit -= PlayerHit;
        EventDispatcher.OnPlayerKilled -= PlayerKilled;
        EventDispatcher.OnEnemyKilled -= EnemyKilled;
    }
}
