using UnityEngine;

[RequireComponent(typeof(IPoolable))]
public class ReturnToPoolWhenOffscreen : MonoBehaviour
{
    [SerializeField] private float offset;

    IPoolable poolableObj;
    private float yPosViewport;

    private void Awake()
    {
        poolableObj = GetComponent<IPoolable>();
    }

    private void Update()
    {
        yPosViewport = Camera.main.WorldToViewportPoint(transform.position).y;
        if (yPosViewport < (0f - offset) || yPosViewport > (1 + offset))
        {
            poolableObj.ReturnToPool();
        }
    }

}
