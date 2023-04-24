using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMove : MonoBehaviour
{

    [SerializeField] Vector3 speed;

    void Update()
    {
        //Linear move.
        transform.position += speed * Time.deltaTime;
    }
}
