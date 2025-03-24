using UnityEngine;
using UnityEngine.Events;

public class PowerCollection : MonoBehaviour
{
    private bool used = false;
    private int powerValue = 5;
    public UnityEvent<int> powerTouched;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }
        if (used) { return; }
        powerTouched.Invoke(powerValue);
        used = true;
        gameObject.SetActive(false);
    }
}