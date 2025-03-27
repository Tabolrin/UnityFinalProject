using UnityEngine;
using UnityEngine.Events;

public class EventCaller : MonoBehaviour
{
    public UnityEvent theEvent;
    public void InvokeShoot()
    {
        theEvent.Invoke();
    }
}
