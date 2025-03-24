using UnityEngine;
using UnityEngine.Events;

public class HealthOrb : MonoBehaviour
{
    [SerializeField] HealthParameters healthParameters;

    private bool used = false;
    public static UnityAction<int> onHealthOrbTouched;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }

        if (used) { return; }

        onHealthOrbTouched.Invoke(healthParameters.healAmount);
      
        gameObject.SetActive(false);

        used = true;
    }
}
