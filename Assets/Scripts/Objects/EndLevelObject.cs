using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelObject : MonoBehaviour
{
    [SerializeField] string SceneToLoad;
    [SerializeField] SceneHandler sceneHandler;
    [SerializeField] SaveLoadManager saveLoadManager;
    
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlaySound(AudioManager.SoundClips.LevelWinSfx);
        
        if(SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
            saveLoadManager.SaveGame();
        else if (SceneManager.GetActiveScene().buildIndex == 3)
            saveLoadManager.DeleteSaveFile();
        
        if (other.CompareTag("Player"))
            sceneHandler.Load(SceneToLoad);
    }
}
