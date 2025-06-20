using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public TMP_Text text;
    public int currentScore;

    private void Start()
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

    public void AddScore(int amount)
    {
        Debug.Log($"Score: {currentScore}");
        currentScore += amount; 
        PlayerPrefs.SetInt("Score", currentScore + amount); 
        
        Debug.Log($"Score += {amount}");
        Debug.Log($"Score: {currentScore + amount}");
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