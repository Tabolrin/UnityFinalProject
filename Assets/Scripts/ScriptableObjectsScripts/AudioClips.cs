using UnityEngine;

[CreateAssetMenu(fileName = "AudioClips", menuName = "Scriptable Objects/AudioClips")]
public class AudioClips : ScriptableObject
{
    [Header("---------------Music---------------")]
    public AudioClip MainMenuBgMusic;
    public AudioClip WinBgMusic;
    public AudioClip LoseBgMusic;
    public AudioClip MazeBgMusic;
    public AudioClip BossRoomBgMusic;
    
    [Header("---------------SFX---------------")]
    public AudioClip PlayerStepsSfx;
    public AudioClip PlayerAttackSfx;
    public AudioClip RangedEnemyAttackSfx;
    public AudioClip MeleeEnemyAttackSfx;
    public AudioClip PowerUpCollectedSfx;
}
