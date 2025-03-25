using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
   [SerializeField] private AudioMixer _audioMixer;
   [SerializeField] private Slider _volumeSlider;

   public void SetMasterVolume()
   {
      float volume = _volumeSlider.value;
      _audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
   }
   
   public void SetMusicVolume()
   {
      float volume = _volumeSlider.value;
      //_audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
      _audioMixer.SetFloat("MusicVolume", volume);
   }
   
   public void SetSfxVolume()
   {
      float volume = _volumeSlider.value;
      _audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
   }
}
