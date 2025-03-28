using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum Scenes
    {
        Opening = 0,
        Level1,
        Level2,
        Level3,
        Lose,
        Win
    };
    
    //[SerializeField] PrefabHolderSO prefubHolderSO;
    //[SerializeField] HealthParameters healthParameters;

    private Scenes currentScene;
    
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        currentScene = (Scenes)SceneManager.GetActiveScene().buildIndex;

        switch (currentScene)
        {
            case Scenes.Opening:
            case Scenes.Level1:
            case Scenes.Level2:
            {
                AudioManager.Instance.PlaySound(AudioManager.SoundClips.MainMenuAndMazeBgMusic);
                break;
            }

            case Scenes.Level3:
            {
                AudioManager.Instance.PlaySound(AudioManager.SoundClips.BossRoomBgMusic);
                break;
            }

            case Scenes.Lose:
            {
                AudioManager.Instance.PlaySound(AudioManager.SoundClips.LoseBgMusic);
                break;
            }

            case Scenes.Win:
            {
                AudioManager.Instance.PlaySound(AudioManager.SoundClips.WinBgMusic);
                break;
            }
        }
    }
}
