using System;
using TMPro;
using UnityEngine;

public class EndingSceneManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TMP_Text  finalScoreText;

    private void Awake()
    {
        HighScore();
    }

    private void HighScore()
    {
        if (finalScoreText != null)
        {
            if (scoreManager.SaveHighScore())
                finalScoreText.text = $"New HighScore! \n Final Score: {scoreManager.currentScore}";
            else 
                finalScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore")} \n Final Score: {scoreManager.currentScore}";
        }
    }
}
