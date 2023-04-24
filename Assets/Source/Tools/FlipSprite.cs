using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlipSprite : MonoBehaviour
{

    [SerializeField] private float frequency;
    [SerializeField] private bool flipX;
    [SerializeField] private bool flipY;

    private SpriteRenderer sprtRend;
    private float timer;

    private void Start()
    {
        sprtRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Flip sprite in X and or Y with set frequency.
        if (timer >= frequency)
        {
            if (flipX) sprtRend.flipX = !sprtRend.flipX;
            if (flipY) sprtRend.flipY = !sprtRend.flipY;
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
}
