using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]  HealthParameters healthParameters;

    public int health = 100;
    public static UnityAction<int> OnHealthUpdated;

    private void Awake()
    {
        HealthOrb.onHealthOrbTouched += AddHealth;
        SpikeTrap.onSpikeTouched += TakeDamage;
    }

    private void OnDestroy()
    {
        HealthOrb.onHealthOrbTouched -= AddHealth;
        SpikeTrap.onSpikeTouched -= TakeDamage;
    }

    public void AddHealth(int amount)
    {
        if (health <= 0) { return; }
        health = math.min(healthParameters.MaxHealth, health + amount);
        OnHealthUpdated.Invoke(health);
    }

    public void TakeDamage(int amount)
    {
        health = math.max(health - amount, healthParameters.MinHealth);
        if (health == healthParameters.MinHealth) { PlayerDied(); }
        OnHealthUpdated.Invoke(health);
    }

    private void PlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
