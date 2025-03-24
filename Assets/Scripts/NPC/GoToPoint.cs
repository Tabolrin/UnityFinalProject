using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class GoToPoint : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    private float playerYPos;

    private void Awake()
    {
        playerYPos = transform.position.y;
    }

    public void GoToTarget(Vector3 target)
    {
        Vector3 newTarget = target;
        newTarget.y = playerYPos;

        agent.destination = newTarget;
    }
}
