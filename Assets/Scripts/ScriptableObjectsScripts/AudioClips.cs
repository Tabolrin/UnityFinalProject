using UnityEngine;

[CreateAssetMenu(fileName = "AudioClips", menuName = "Scriptable Objects/AudioClips")]
public class AudioClips : ScriptableObject
{
    [Header("---------------Music---------------")]
    public AudioClip WinBgMusic;
    public AudioClip LoseBgMusic;
    public AudioClip MainMenuAndMazeBgMusic;
    public AudioClip BossRoomBgMusic;
    
    [Header("---------------SFX---------------")]
    public AudioClip PlayerHurtSfx;
    public AudioClip PlayerAttackSfx;
    public AudioClip EnemyAttackSfx;
    public AudioClip EnemyHurtSfx;
    public AudioClip PowerUpCollectedSfx;
    public AudioClip PlayerDeathSfx;
    public AudioClip LevelWinSfx;
}
