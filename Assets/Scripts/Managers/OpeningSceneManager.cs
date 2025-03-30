using UnityEngine;

public class OpeningSceneManager : MonoBehaviour
{
    public GameObject loadButton;
    
    void Awake()
    {
        PlayerPrefs.SetInt("Score", 0);
        
        if (SaveLoadManager.HasActiveSave)
            loadButton.SetActive(true);
        else
            loadButton.SetActive(false);
    }
}
