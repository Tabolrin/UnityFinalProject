using System;
using TMPro;
using UnityEngine;

public class EndingSceneManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TMP_Text  finalScoreText;
    public GameObject loadButton;

    private void Awake()
    {
        if (loadButton != null)
        {
            if (SaveLoadManager.HasActiveSave)
                loadButton.SetActive(true);
            else
                loadButton.SetActive(false);
        }
    }

    private void Start()
    {
        HighScore();
    }

    private void HighScore()
    {
        if (finalScoreText != null)
        {
            if (scoreManager.SaveHighScore())
                finalScoreText.text = $"New HighScore! \n Final Score: {PlayerPrefs.GetInt("FinalScore")}";
            else 
                finalScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore")} \n Final Score: {PlayerPrefs.GetInt("FinalScore")}";
            
            PlayerPrefs.DeleteKey("FinalScore");
        
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("No Final Score");
        }
    }
}
