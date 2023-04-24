using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreTracker : MonoBehaviour
{

    private Text scoreText;
    private int currentScore;

    private void Awake()
    {
        EventDispatcher.OnScoreGained += ScoreGained;
        scoreText = GetComponent<Text>();
    }

    private void ScoreGained(int score)
    {
        currentScore += score;
        scoreText.text = currentScore.ToString();
    }

    private void OnDestroy()
    {
        EventDispatcher.OnScoreGained -= ScoreGained;
    }
}
