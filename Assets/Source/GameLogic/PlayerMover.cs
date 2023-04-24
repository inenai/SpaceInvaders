using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMover : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private float edgeOffset = 0.05f;

    private float minXPos;
    private float maxXPos;

    private void Start()
    {
        SpriteRenderer sprtRend = GetComponent<SpriteRenderer>();
        minXPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        maxXPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        //If these were the limits, the player would be halfways out of the screen when at the edges.
        //Adding offset, composed of half the horizontal size of the player, plus an extra offset:
        float totalOffset = sprtRend.bounds.size.x / 2f + edgeOffset;
        minXPos += totalOffset;
        maxXPos -= totalOffset;
    }

    private void Update()
    {
        float value = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(value) > 0.1f)
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + Time.deltaTime * value * speed, minXPos, maxXPos), transform.position.y, transform.position.z);
    }
}
