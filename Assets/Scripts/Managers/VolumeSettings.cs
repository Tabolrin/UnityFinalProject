using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
   [SerializeField] private AudioMixer _audioMixer;
   [SerializeField] private Slider _volumeSlider;

   //Unused, kept for possible future use
   public void SetMasterVolume()
   {
      float volume = _volumeSlider.value;
      _audioMixer.SetFloat("MasterVolume", volume);
   }
   
   public void SetMusicVolume()
   {
      float volume = _volumeSlider.value;
      _audioMixer.SetFloat("MusicVolume", volume);
      
      if(volume <= -30)
         _audioMixer.SetFloat("MusicVolume", -80f);
   }
   
   public void SetSfxVolume()
   {
      float volume = _volumeSlider.value;
      _audioMixer.SetFloat("SfxVolume", volume);
      
      if(volume <= -30)
         _audioMixer.SetFloat("SfxVolume", -80f);
   }
}
