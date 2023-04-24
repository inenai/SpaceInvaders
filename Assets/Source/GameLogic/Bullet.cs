using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{

    private void Awake()
    {
        EventDispatcher.OnEnemyReset += EnemiesReset;
    }

    private void Start()
    {
        GetComponent<AudioSource>().Play(); //Enemy fire SFX
    }

    private void EnemiesReset()
    {
        Destroy(gameObject); //Bullets disappear when enemies are reset
    }

    public void DestroyBullet()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        if (GetComponent<AudioSource>().isPlaying)
        {
            StartCoroutine(WaitForSoundToEnd()); //Avoid interrupting SFX.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator WaitForSoundToEnd()
    {
        while (GetComponent<AudioSource>().isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventDispatcher.OnEnemyReset -= EnemiesReset;
    }
}
