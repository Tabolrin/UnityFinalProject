using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundClips 
    { 
        //Music
        WinBgMusic,
        LoseBgMusic,
        MainMenuAndMazeBgMusic,
        BossRoomBgMusic,
        //Sfx
        PlayerHurtSfx,
        PlayerAttackSfx,
        BossAttackSfx,
        PlayerDeathSfx,
        EnemyAttackSfx,
        EnemyHurtSfx,
        PowerUpCollectedSfx,
        LevelWinSfx
    };

    public AudioSource MusicAudioSource;
    public AudioSource SFXAudioSource;
    public AudioClips AudioClipsContainer;
    
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(SoundClips actionSound)
    {
        switch (actionSound)
        {
            //===============Music===============
            //---------Main Menu & Maze Levels--------
            case SoundClips.MainMenuAndMazeBgMusic:
            {
                MusicAudioSource.clip = AudioClipsContainer.MainMenuAndMazeBgMusic;
                MusicAudioSource.Play();
                break;
            }
            
            //---------End Screens--------
            case SoundClips.LoseBgMusic:
            {
                MusicAudioSource.clip = AudioClipsContainer.LoseBgMusic;
                MusicAudioSource.Play();                break;
            }

            case SoundClips.WinBgMusic:
            {
                MusicAudioSource.clip = AudioClipsContainer.WinBgMusic;
                MusicAudioSource.Play();
                break;
            }
            
            //---------Boss Level--------
            case SoundClips.BossRoomBgMusic:
            {
                MusicAudioSource.clip = AudioClipsContainer.BossRoomBgMusic;
                MusicAudioSource.Play();               
                break;
            }
            
            //===============Sfx===============
            //---------PowerUps--------
            case SoundClips.PowerUpCollectedSfx:
            {
                SFXAudioSource.clip = AudioClipsContainer.PowerUpCollectedSfx;
                SFXAudioSource.Play();  
                break;
            }
            
            //---------Player Sfx--------
            case SoundClips.PlayerAttackSfx:
            {
                SFXAudioSource.clip = AudioClipsContainer.PlayerAndBossAttackSfx;
                SFXAudioSource.Play();  
                break;
            }
            
            case SoundClips.PlayerDeathSfx:
            {
                SFXAudioSource.clip = AudioClipsContainer.PlayerDeathSfx;
                SFXAudioSource.Play();  
                break;
            }
            
            case SoundClips.PlayerHurtSfx:
            {
                SFXAudioSource.clip = AudioClipsContainer.PlayerHurtSfx;
                SFXAudioSource.Play();  
                break;
            }
            
            //---------Enemy & Boss Sfx--------
            case SoundClips.EnemyAttackSfx:
            {
                SFXAudioSource.clip = AudioClipsContainer.EnemyAttackSfx;
                SFXAudioSource.Play();  
                break;
            }
            
            case SoundClips.EnemyHurtSfx:
            {
                SFXAudioSource.clip = AudioClipsContainer.EnemyHurtSfx;
                SFXAudioSource.Play();  
                break;
            }
            
            case SoundClips.BossAttackSfx:
            {
                SFXAudioSource.clip = AudioClipsContainer.PlayerAndBossAttackSfx;
                SFXAudioSource.Play();  
                break;
            }
            
            //---------Level End Reached--------
            case SoundClips.LevelWinSfx:
            {
                SFXAudioSource.clip = AudioClipsContainer.LevelWinSfx;
                SFXAudioSource.Play();  
                break;
            }
        }
    }
}
