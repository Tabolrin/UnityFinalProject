using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField] public TMP_Text text;
    private int currentScore = 0;

    private void Awake()
    {
        text.text = currentScore.ToString();
    }

    public void UpdateScoreText(int amount)
    {
        currentScore += amount;
        text.text = $"Score - {currentScore}"; 
    }
}
