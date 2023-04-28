using System;
using UnityEngine;
using UnityEngine.Pool;

public enum BULLET_TYPE 
{
    PLAYER_BULLET,
    ENEMY_BULLET
}

public class BulletPool : MonoBehaviour
{   
    private ObjectPool<Bullet> playerPool;
    private ObjectPool<Bullet> enemyPool;
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab;

    private void Awake()
    {
        playerPool = new ObjectPool<Bullet> (CreatePlayerBullet, OnTakeBulletFromPool, OnReturnBulletToPool);
        enemyPool = new ObjectPool<Bullet>(CreateEnemyBullet, OnTakeBulletFromPool, OnReturnBulletToPool);
    }

    public Bullet CreateBullet(BULLET_TYPE type)
    {
        switch (type) 
        {
            case BULLET_TYPE.PLAYER_BULLET:
                return CreatePlayerBullet();
            case BULLET_TYPE.ENEMY_BULLET:
                return CreateEnemyBullet();
            default:
                throw new Exception("unknown bullet type: " + type.ToString());
        }       
    }

    public Bullet GetPlayerBullet() 
    {
        return playerPool.Get();
    }

    public Bullet GetEnemyBullet()
    {
        return enemyPool.Get();
    }

    public void ReleasePlayerBullet(Bullet bullet)
    {
        playerPool.Release(bullet);
    }

    public void ReleaseEnemyBullet(Bullet bullet)
    {
       enemyPool.Release(bullet);
    }

    private Bullet CreatePlayerBullet() 
    {
        Bullet bullet = Instantiate(playerBulletPrefab).GetComponent<Bullet>();
        bullet.SetPool(playerPool);
        return bullet;
    }

    private Bullet CreateEnemyBullet()
    {
        Bullet bullet = Instantiate(enemyBulletPrefab).GetComponent<Bullet>();
        bullet.SetPool(enemyPool);
        return bullet;
    }

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        bullet.SetActive(true);       
    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.SetActive(false);
    }

}
