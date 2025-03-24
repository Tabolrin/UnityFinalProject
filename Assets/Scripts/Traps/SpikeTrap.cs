using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] HealthParameters healthParameters;

    private float tickAfterTouch;
    private float touchCoolDown = 1f;

    public static UnityAction<int> onSpikeTouched;

    private void Awake()
    {
        tickAfterTouch = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }

        if (tickAfterTouch > Time.time) { return; }

        tickAfterTouch = Time.time + touchCoolDown;

        onSpikeTouched.Invoke(healthParameters.SpikeDamageAmount);
    }
}
