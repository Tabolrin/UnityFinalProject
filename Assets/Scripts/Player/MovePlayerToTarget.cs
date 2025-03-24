using UnityEngine;
using UnityEngine.Events;

public class MovePlayerToTarget : MonoBehaviour
{
    public Camera mainCamera;
    public UnityEvent<Vector3> targetLocation;
    private float currentCoolDownTick;

    private void Awake()
    {
        currentCoolDownTick = Time.time;
    }

    void Update()
    {
        if (Time.time < currentCoolDownTick) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            currentCoolDownTick = Time.time;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 worldPosition = hit.point;

                worldPosition.y = hit.point.y;

                targetLocation.Invoke(worldPosition);
            }
        }
    }
}