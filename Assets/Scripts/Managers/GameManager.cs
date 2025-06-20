using System;
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

    private Scenes currentScene;
    
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += UpdateMusic;
    }

    private void UpdateMusic(Scene scene, LoadSceneMode mode)
    {
        currentScene = (Scenes)scene.buildIndex;

        switch (currentScene)
        {
            case Scenes.Opening:
            case Scenes.Level1:
            case Scenes.Level2:
            {
                if ( AudioManager.Instance.MusicAudioSource.clip != AudioManager.Instance.AudioClipsContainer.MainMenuAndMazeBgMusic)
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

   

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= UpdateMusic;
    }
}
