using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{

    [SerializeField] private Transform firePivot;
    [SerializeField] private SpriteRenderer sprtRend;

    [Header("Configuration")]
    [SerializeField] private int initialLife = 3;
    [SerializeField] private float blinkDuration = 0.05f;

    [Header("References")]
    [SerializeField] private LivesCounter livesCounter;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject explosion;
    [SerializeField] private BulletPool bulletPool;

    private bool shot;
    private int currentLife;
    private bool onPause;
    private bool playerDead;
    private Coroutine hitCoroutine = null;

    private void Awake()
    {
        EventDispatcher.OnPauseMenuOpen += ToggleFire;
    }

    private void Start()
    {
        currentLife = initialLife;
        livesCounter.Setup(initialLife);
    }

    private void Update()
    {
        if (Input.GetButton("Fire") && !shot)
        {
            if (!(onPause || playerDead))
            {
                float xCoord = Camera.main.WorldToViewportPoint(transform.position).x;
                EventDispatcher.PlayerShot(xCoord);
                Bullet bullet = bulletPool.GetPlayerBullet();
                bullet.gameObject.transform.position = firePivot.position;
                shot = true;
            }
        }
        else if (!Input.GetButton("Fire")) //Disable continuous bullet instantiation.
        {
            shot = false;
        }
    }

    private void ToggleFire(bool onPause)
    {
        this.onPause = onPause;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("EnemyBullet") || other.tag.Equals("Enemy"))
        {
            currentLife--;
            livesCounter.RemoveLife();
            if (currentLife == 0)
            {
                StartCoroutine(EndGame());
            }
            else
            {
                if (hitCoroutine != null)
                {
                    StopCoroutine(hitCoroutine); //Reseting blinking invincibility coroutine.
                }
                hitCoroutine = StartCoroutine(Hit());
            }
            if (other.tag.Equals("EnemyBullet"))
            {
                other.GetComponent<Bullet>().DestroyBullet();
            }
        }
    }

    private IEnumerator EndGame()
    { //Shows player exploding before ending game.
        playerDead = true;
        GetComponent<PlayerMover>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        explosion.SetActive(true);
        float xCoord = Camera.main.WorldToViewportPoint(transform.position).x;
        EventDispatcher.PlayerKilled(xCoord);
        yield return new WaitForSeconds(1f);
        explosion.SetActive(false);
        yield return new WaitForSeconds(1f);
        sceneLoader.LoadScene(SceneLoader.MAIN_MENU_SCENE);
    }

    private IEnumerator Hit() //Blinking invincibility effect on hit.
    {
        float xCoord = Camera.main.WorldToViewportPoint(transform.position).x;
        EventDispatcher.PlayerHit(xCoord);
        GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < 3; i++)
        {
            sprtRend.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(blinkDuration);
            sprtRend.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(blinkDuration);
        }
        GetComponent<BoxCollider>().enabled = true;
        hitCoroutine = null;
    }

    private void OnDestroy()
    {
        EventDispatcher.OnPauseMenuOpen -= ToggleFire;
    }
}
