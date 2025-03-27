using UnityEngine;
using UnityEngine.Events;

public class ShootEvent : MonoBehaviour
{
    public UnityEvent shoot;
    public void InvokeShoot()
    {
        shoot.Invoke();
    }
}
