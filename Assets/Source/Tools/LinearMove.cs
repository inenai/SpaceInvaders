using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMove : MonoBehaviour
{

    [SerializeField] private Vector3 speed;

    private void Update()
    {
        //Linear move.
        transform.position += speed * Time.deltaTime;
    }
}
