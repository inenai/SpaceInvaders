using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Invokes action when transform + offset is out of the screen.
/// </summary>
public class ActionWhenOffscreen : MonoBehaviour
{

    [SerializeField] private float offset;
    [SerializeField] private UnityEvent action;
    float yPosViewport;

    private void Update()
    {
        yPosViewport = Camera.main.WorldToViewportPoint(transform.position).y;
        if (yPosViewport < (0f - offset) || yPosViewport > (1 + offset))
        {
            action.Invoke();
        }
    }
}
