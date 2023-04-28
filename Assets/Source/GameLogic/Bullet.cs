using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour, IPoolable
{
    private ObjectPool<Bullet> pool;
    private bool activeBullet;

    private void Awake()
    {
        EventDispatcher.OnEnemyReset += EnemiesReset;
    }

    public void SetPool(ObjectPool<Bullet> pool) {
        this.pool = pool;
    }

    public void SetActive(bool active)
    {
        activeBullet = active;
        GetComponent<BoxCollider>().enabled = active;
        GetComponent<SpriteRenderer>().enabled = active;
        gameObject.SetActive(active);
    }

    public void ReturnToPool()
    {
        DestroyBullet();
    }

    public void DestroyBullet()
    {
        if (activeBullet)
        {
            SetActive(false);
            pool.Release(this);
        }
    }

    private void EnemiesReset()
    {
        DestroyBullet(); //Bullets disappear when enemies are reset
    }

    private void OnDestroy()
    {
        EventDispatcher.OnEnemyReset -= EnemiesReset;
    }

}
