using System.Collections;
using UnityEngine;

namespace Enemies
{

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(EnemyMover))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform firePivot;

        [Header("Configuration")]
        [SerializeField] private float idleFrequency = 0.8f;

        [Header("References")]
        [SerializeField] private Sprite[] idleSprites;
        [SerializeField] private Sprite dieSprite;
        [SerializeField] private GameObject bulletPrefab;

        private SpriteRenderer sprtRend;
        public int rowIndex { get; private set; }
        public int columnIndex { get; private set; }
        private int enemyKind;
        private int score;
        private Color enemyColor;

        private int currentLife;
        private int idleSpriteIndex;
        private float timer;
        private bool exploded;

        private void Awake()
        {
            sprtRend = GetComponent<SpriteRenderer>();
        }

        public void Setup(EnemyData data, int row, int column)
        {
            enemyKind = data.Kind;
            enemyColor = data.Color; //Could ask for color to EnemyRepository using ID each time, 
                                     //but in this case storing a color doesn't take up too much memory.
            sprtRend.color = enemyColor;
            currentLife = data.Life;
            score = data.Score;
            rowIndex = row;
            columnIndex = column;
        }

        private void Update()
        {
            //Animating enemies through a script and not an animator because an animator 
            //seems too much for a simple sprite swap animation.
            if (timer >= idleFrequency && currentLife > 0)
            {
                SwapIdleSprite();
                timer -= idleFrequency;
            }
            timer += Time.deltaTime;
        }

        private void SwapIdleSprite()
        {
            //Cycle through idle animation sprites.
            idleSpriteIndex++;
            sprtRend.sprite = idleSprites[idleSpriteIndex % idleSprites.Length];
        }

        public void Shoot()
        {
            if (!HasExploded())
            {
                Instantiate(bulletPrefab, firePivot.position, Quaternion.identity);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("PlayerBullet"))
            {
                currentLife--;
                if (currentLife == 0)
                {
                    Explode();
                }
                else
                {
                    if (hitCoroutine != null)
                    {
                        StopCoroutine(hitCoroutine);
                    }
                    hitCoroutine = StartCoroutine(Hit());
                }
                other.GetComponent<Bullet>().DestroyBullet();
            }
        }

        private Coroutine hitCoroutine = null;
        private IEnumerator Hit()
        {
            sprtRend.color = Color.white;
            yield return new WaitForSeconds(idleFrequency / 2f);
            sprtRend.color = enemyColor;
            hitCoroutine = null;
        }

        public void AutoKill()
        {
            Explode(true);
        }

        /// <summary>
        /// Explode marks the enemy as killed and sends an event that EnemiesController will receive to process this kill.
        /// </summary>
        /// <param name="autoKill"></param>
        public void Explode(bool autoKill = false)
        {
            if (!autoKill) //Gives score only if was killed by player.
            {
                EventDispatcher.ScoreGained(score);
            }
            currentLife = 0;
            GetComponent<EnemyMover>().enabled = false;
            if (hitCoroutine != null)
            {
                StopCoroutine(hitCoroutine); //Reset hit sprite color change coroutine.
                hitCoroutine = null;
            }
            if (!autoKill) //Only trigger checking for same-type neighbours if death cause wasn't reaching the bottom of the screen.
            {
                EventDispatcher.EnemyKilled(rowIndex, columnIndex);
            }
            exploded = true; //Mark as killed.
            sprtRend.color = enemyColor; //Avoid white explosion if was being hit when killed
            sprtRend.sprite = dieSprite;
            GetComponent<AudioSource>().Play();
            StartCoroutine(DestroyEnemy());
        }

        private IEnumerator DestroyEnemy()
        {
            GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(idleFrequency); //So exploding effect will last enough to be properly seen.
            GetComponent<SpriteRenderer>().enabled = false;
            while (GetComponent<AudioSource>().isPlaying) //So sound won't be interrupted.
            {
                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject); //Improvable: Could use disable instead of destroy and reuse+reset assets each game.
        }

        public bool HasExploded()
        {
            return exploded;
        }

        public int GetKind()
        {
            return enemyKind;
        }
    }
}