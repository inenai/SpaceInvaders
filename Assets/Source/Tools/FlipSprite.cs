using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlipSprite : MonoBehaviour
{

    [SerializeField] float frequency;
    [SerializeField] bool flipX;
    [SerializeField] bool flipY;

    SpriteRenderer sprtRend;
    float timer;

    private void Start()
    {
        sprtRend = GetComponent<SpriteRenderer>();
    }

    void Update()
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
