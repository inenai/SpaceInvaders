using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour {

	public void Quit() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false; //Forces game to stop in Unity Editor.
#else
        Application.Quit(); //Forces game to quit.
#endif
	}
}
