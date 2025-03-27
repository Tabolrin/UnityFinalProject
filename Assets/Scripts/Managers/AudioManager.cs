using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundClips 
    { 
        //Music
        MainMenuBgMusic,
        WinBgMusic,
        LoseBgMusic,
        MazeBgMusic,
        BossRoomBgMusic,
        //Sfx
        PlayerStepsSfx,
        PlayerAttackSfx,
        RangedEnemyAttackSfx,
        MeleeEnemyAttackSfx,
        PowerUpCollectedSfx
    };

    [SerializeField] private GameObject Music;
    [SerializeField] private GameObject SFX;
    [SerializeField] AudioClips AudioClipsContainer;
    [SerializeField] int delayTime;
    
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

        StartCoroutine(PlayAudioWithDelay(2f));
    }

    private IEnumerator PlayAudioWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        //_audioSource.PlayOneShot(AIVoiceLine);
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
            case SoundClips.MazeBgMusic:
            {
                _musicAudioSource.PlayOneShot(AudioClipsContainer.MainMenuAndMazeBgMusic);
                break;
            }
            
            
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

            case SoundClips.BossRoomBgMusic:
            {
                _musicAudioSource.PlayOneShot(AudioClipsContainer.BossRoomBgMusic);
                break;
            }
        }
    }
}
