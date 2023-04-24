using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreTracker : MonoBehaviour {

	Text scoreText;
	int currentScore;

	void Awake() {
		EventDispatcher.OnScoreGained += ScoreGained;
		scoreText = GetComponent<Text>();
	}

	void ScoreGained(int score) {
		currentScore += score;
		scoreText.text = currentScore.ToString();
	}

	void OnDestroy() {
		EventDispatcher.OnScoreGained -= ScoreGained;
	}
}
