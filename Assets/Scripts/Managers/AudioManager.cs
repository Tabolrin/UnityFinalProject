using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundClips 
    { 
        //Music
        ColorSortBGMusic,
        EmotionMatchBGMusic,
        MainMenuBGMusic,
        KindergartenBGMusic,
        //Sfx
        ColorMatchSuccessSound,
        BackToDefaultPositionSound,
        EmotionMatchSuccessSound,
        LineDisappearSound,
        //AI Voicelines
        AIVoiceLine,
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

    public void StopBackgroundMusic()
    {
        _musicAudioSource.Stop();
    }

    public void PlaySound(SoundClips actionSound)
    {
        switch (actionSound)
        {
            //---------Color Sort--------
            case SoundClips.ColorSortBGMusic:
            {
                //_musicAudioSource.PlayOneShot(AudioClipsContainer.ColorSortBGMusic);
                _musicAudioSource.clip = AudioClipsContainer.ColorSortBGMusic;
                _musicAudioSource.Play();
                break;
            }
            
            case SoundClips.ColorMatchSuccessSound:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.ColorMatchSuccessSfx);
                break;
            }

            case SoundClips.BackToDefaultPositionSound:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.ColorMatchLocationResetSfx);
                break;
            }
            
            //---------Emotion Match--------
            case SoundClips.EmotionMatchBGMusic:
            {
                _musicAudioSource.PlayOneShot(AudioClipsContainer.EmotionMatchBGMusic);
                break;
            }
            
            case SoundClips.EmotionMatchSuccessSound:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.EmotionMatchSuccessSfx);
                break;
            }
            
            case SoundClips.LineDisappearSound:
            {
                _sfxAudioSource.PlayOneShot(AudioClipsContainer.LineDisappearSfx);
                break;
            }
            
            //---------Main Menu--------
            case SoundClips.MainMenuBGMusic:
            {
                _musicAudioSource.PlayOneShot(AudioClipsContainer.MainMenuBGMusic);
                break;
            }
            
            //---------Kindergarten--------
            case SoundClips.KindergartenBGMusic:
            {
                _musicAudioSource.PlayOneShot(AudioClipsContainer.KindergartenBGMusic);
                break;
            }
            
        }
    }
}
