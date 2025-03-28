using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public TMP_Text text;
    public int currentScore = 0;

    private void Awake()
    {
        text.text = currentScore.ToString();
    }

    public void UpdateScoreText(int amount)
    {
        currentScore += amount;
        text.text = $"Score: {currentScore}";
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
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
        
        PlayerPrefs.Save();
        
        return highScoreAchieved;
    }
}