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

    private void PlayerShot(float xCoord) {
        playerShotSrc.panStereo = (xCoord * 2f) - 1f;
        playerShotSrc.Play();
    }

    private void EnemyShot(float xCoord)
    {
        enemyShotSrc.panStereo = (xCoord * 2f) - 1f;
        enemyShotSrc.Play();
    }

    private void PlayerHit(float xCoord)
    {
        playerHitSrc.panStereo = (xCoord * 2f) - 1f;
        playerHitSrc.Play();
    }

    private void PlayerKilled(float xCoord)
    {
        playeKilledSrc.panStereo = (xCoord * 2f) - 1f;
        playeKilledSrc.Play();
    }

    private void EnemyKilled(int rowIndex, int columnIndex, bool autoKill, float xCoord)
    {
        enemyKilledSrc.panStereo = (xCoord * 2f) - 1f;
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
