using UnityEngine;

public class OpeningSceneManager : MonoBehaviour
{
    public GameObject loadButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        
        if (SaveLoadManager.HasActiveSave)
            loadButton.SetActive(true);
        else
            loadButton.SetActive(false);
    }
}
