using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField] public TMP_Text text;
    private int currentPowerScore = 0;

    private void Awake()
    {
        text.text = currentPowerScore.ToString();
    }

    public void UpdateScoreText(int amount)
    {
        currentPowerScore += amount;
        text.text = $"Score - {currentPowerScore}"; 
    }
}
