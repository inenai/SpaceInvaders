using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys gameObject when transform + offset is out of the screen.
/// </summary>
public class DestroyWhenOffscreen : MonoBehaviour {

	[SerializeField] float offset;
	float yPosViewport;

	void Update() {
		yPosViewport = Camera.main.WorldToViewportPoint(transform.position).y;
		if (yPosViewport < (0f - offset) || yPosViewport > (1 + offset))
			Destroy(gameObject);
	}
}
