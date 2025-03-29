using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public TMP_Text text;
    public int currentScore;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Score"))
            PlayerPrefs.SetInt("Score", 0);
        else
            currentScore = PlayerPrefs.GetInt("Score");
        
        if (text != null)
            text.text = $"Score: {currentScore}";
    }

    private void Update()
    {
        if (text != null)
            text.text = $"Score: {currentScore}";
    }

    public void UpdateScoreText(int amount)
    {
        currentScore = PlayerPrefs.GetInt("Score");
        currentScore += amount;
        if (text != null)
            text.text = $"Score: {currentScore}";
    }

    public void AddScore(int amount)
    {
        currentScore = PlayerPrefs.GetInt("Score");
        PlayerPrefs.SetInt("Score", currentScore + amount); 
        currentScore = PlayerPrefs.GetInt("Score");
    }

    public bool SaveHighScore()
    {
        bool highScoreAchieved = false;
        
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (currentScore > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", currentScore);
                highScoreAchieved = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScoreAchieved = true;
        }
        
        PlayerPrefs.SetInt("FinalScore", currentScore);
        PlayerPrefs.DeleteKey("Score");
        
        PlayerPrefs.Save();
        
        return highScoreAchieved;
    }
}