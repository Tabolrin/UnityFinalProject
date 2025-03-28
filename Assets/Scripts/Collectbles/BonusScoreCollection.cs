using UnityEngine;
using UnityEngine.Events;

public class BonusScoreCollection : MonoBehaviour
{
    public ScoreManager scoreManager;
    private bool used = false;
    private int scoreValue = 5;
    //public UnityEvent<int> bonusScoreTouched;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }
        if (used) { return; }
        
        AudioManager.Instance.PlaySound(AudioManager.SoundClips.PowerUpCollectedSfx);
        scoreManager.AddScore(scoreValue);
        //bonusScoreTouched.Invoke(scoreValue);
        used = true;
        gameObject.SetActive(false);
    }
}