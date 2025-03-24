using UnityEngine;

public class AuraController : MonoBehaviour
{
    [SerializeField] private Light auraLightSource;
    [SerializeField] private float auraTime;
    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    private float timeToTurnOffLight = 0;
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timeToTurnOffLight)
            TurnOffAura();
    }

    public void TurnOnAura()
    {
        auraLightSource.intensity = minIntensity;
        auraLightSource.enabled = true;
        timeToTurnOffLight = Time.time + auraTime;
    }
    private void TurnOffAura()
    {
        auraLightSource.enabled = false;
    }
    public void MaximizeAura()
    {
        auraLightSource.intensity = maxIntensity;
    }
}
