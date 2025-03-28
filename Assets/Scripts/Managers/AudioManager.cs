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

    [SerializeField] private GameObject Music;
    [SerializeField] private GameObject SFX;
    [SerializeField] AudioClips AudioClipsContainer;
    
    private AudioSource _musicAudioSource;
    private AudioSource _sfxAudioSource;
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _musicAudioSource = Music.GetComponent<AudioSource>();
        _sfxAudioSource = SFX.GetComponent<AudioSource>();
        
        DontDestroyOnLoad(this.gameObject);
    }

    public void StopBackgroundMusic(bool ShouldPlayMusic)
    {
        if (ShouldPlayMusic)
            _musicAudioSource.Play();
        else
            _musicAudioSource.Stop();
    }

    public void PlaySound(SoundClips actionSound)
    {
        switch (actionSound)
        {
            //===============Music===============
            //---------Main Menu & Maze Levels--------
            case SoundClips.MainMenuAndMazeBgMusic:
            {
                _musicAudioSource.PlayOneShot(AudioClipsContainer.MainMenuAndMazeBgMusic);
                break;
            }
            
            //---------End Screens--------
            case SoundClips.LoseBgMusic:
            {
                _musicAudioSource.PlayOneShot(AudioClipsContainer.LoseBgMusic);
                break;
            }

            case SoundClips.WinBgMusic:
            {
                _musicAudioSource.PlayOneShot(AudioClipsContainer.WinBgMusic);
                break;
            }
            
            //---------Boss Level--------
            case SoundClips.BossRoomBgMusic:
            {
                _musicAudioSource.PlayOneShot(AudioClipsContainer.BossRoomBgMusic);
                break;
            }
            
            //===============Sfx===============
            //---------PowerUps--------
            case SoundClips.PowerUpCollectedSfx:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.PowerUpCollectedSfx);
                break;
            }
            
            //---------Player Sfx--------
            case SoundClips.PlayerAttackSfx:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.PlayerAndBossAttackSfx);
                break;
            }
            
            case SoundClips.PlayerDeathSfx:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.PlayerDeathSfx);
                break;
            }
            
            case SoundClips.PlayerHurtSfx:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.PlayerHurtSfx);
                break;
            }
            
            //---------Enemy & Boss Sfx--------
            case SoundClips.EnemyAttackSfx:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.EnemyAttackSfx);
                break;
            }
            
            case SoundClips.EnemyHurtSfx:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.EnemyHurtSfx);
                break;
            }
            
            case SoundClips.BossAttackSfx:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.PlayerAndBossAttackSfx);
                break;
            }
            
            //---------Level End Reached--------
            case SoundClips.LevelWinSfx:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.LevelWinSfx);
                break;
            }
        }
    }
}
