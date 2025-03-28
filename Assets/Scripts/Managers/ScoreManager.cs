using TMPro;
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
        
        text.text = $"Score: {currentScore.ToString()}";
    }

    public void UpdateScoreText(int amount)
    {
        currentScore = PlayerPrefs.GetInt("Score");
        currentScore += amount;
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
        
        PlayerPrefs.DeleteKey("Score");
        
        PlayerPrefs.Save();
        
        return highScoreAchieved;
    }
}