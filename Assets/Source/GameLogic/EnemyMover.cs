using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField] private float horizontalDistance;
    [SerializeField] private float verticalDistance;
    [SerializeField] private float frequency;
    [SerializeField] private float edgeOffset;

    private SpriteRenderer sprtRend;
    private bool goDown;
    private bool goRight;
    private float timer;

    public void Reset()
    {
        timer = 0f;
        goRight = true;
        goDown = false;
    }

    private void Awake()
    {
        EventDispatcher.OnEnemyReachEdge += OnEnemyReachedEdge;
        sprtRend = GetComponent<SpriteRenderer>();
        goRight = true;
    }

    //If any enemy reached the edge of the screen, next step will be going down, and direction will be the opposite.
    private void OnEnemyReachedEdge(bool goRight)
    {
        goDown = true;
        this.goRight = goRight;
    }

    private void Update()
    {
        if (!goDown)
        {
            float posToCompare = Camera.main.WorldToViewportPoint(transform.position + new Vector3(sprtRend.bounds.size.x / 2f + edgeOffset, 0f, 0f) * (goRight ? 1f : -1f)).x;
            if ((goRight && posToCompare > 1f) || (!goRight) && (posToCompare < 0f))
            {
                EventDispatcher.EnemyReachedLateralEdge(!goRight); //Informs every enemy they should go down and change directions.
            }
        }
    }

    private void LateUpdate() //Separating actual movement in LateUpdate so every enemy will know where to move next.
    {
        timer += Time.deltaTime;
        if (timer > frequency)
        {
            if (goDown)
            {
                transform.position -= new Vector3(0f, horizontalDistance, 0f);
                goDown = false;
            }
            else
            {
                transform.position += new Vector3(goRight ? horizontalDistance : -horizontalDistance, 0f, 0f);
            }
            timer -= frequency;
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.OnEnemyReachEdge -= OnEnemyReachedEdge;
    }
}
