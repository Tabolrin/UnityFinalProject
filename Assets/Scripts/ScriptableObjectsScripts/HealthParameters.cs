using UnityEngine;

[CreateAssetMenu(fileName = "HealthParameters", menuName = "Scriptable Objects/HealthParameters")]
public class HealthParameters : ScriptableObject
{
    [SerializeField] public int PlayerMaxHealth;
    [SerializeField] public int PlayerMinHealth;
    [SerializeField] public int SpikeDamageAmount;
    [SerializeField] public int SkeletonMeleeDamageAmount;
    [SerializeField] public int SkeletonRangedDamageAmount;
    [SerializeField] public int BossMeleeDamageAmount;
    [SerializeField] public int BossRangedDamageAmount;
    [SerializeField] public int healAmount;
}
