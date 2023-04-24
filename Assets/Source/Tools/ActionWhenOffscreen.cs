using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Invokes action when transform + offset is out of the screen.
/// </summary>
public class ActionWhenOffscreen : MonoBehaviour
{

    [SerializeField] float offset;
    [SerializeField] UnityEvent action;
    float yPosViewport;

    void Update()
    {
        yPosViewport = Camera.main.WorldToViewportPoint(transform.position).y;
        if (yPosViewport < (0f - offset) || yPosViewport > (1 + offset))
            action.Invoke();
    }
}
