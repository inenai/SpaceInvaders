using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{

    private void Awake()
    {
        EventDispatcher.OnEnemyReset += EnemiesReset;
    }

    private void EnemiesReset()
    {
        Destroy(gameObject); //Bullets disappear when enemies are reset
    }

    public void DestroyBullet()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject);        
    }

    private void OnDestroy()
    {
        EventDispatcher.OnEnemyReset -= EnemiesReset;
    }
}
