using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GoToPoint goToPoint;
    public WitchPlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goToPoint.GoToTarget(player.transform.position);
    }
}
