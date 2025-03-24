using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] HealthParameters healthParameters;
    [SerializeField] private Slider slider;
    private PlayerHealth playerData;

    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player GameObject not found!");
            return;
        }

        playerData = player.GetComponent<PlayerHealth>();

        if (playerData == null)
        {
            Debug.LogError("Player component not found!");
            return;
        }

        slider.maxValue = healthParameters.MaxHealth;
        slider.minValue = healthParameters.MinHealth;

        slider.value = playerData.health;

        PlayerHealth.OnHealthUpdated += UpdateUi;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnHealthUpdated -= UpdateUi;
    }

    private void UpdateUi(int currentHealth)
    {
        slider.value = currentHealth;
        slider.maxValue = healthParameters.MaxHealth;
    }
}
