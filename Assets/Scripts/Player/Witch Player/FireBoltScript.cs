using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FireBoltScript : MonoBehaviour
{
    private const float Y_LEVEL = 1;
     
    [SerializeField] private Rigidbody rb;
    public UnityEvent<EnemyController> hitAnEnemy;
    public UnityEvent<WitchPlayerController> hitPlayer;
    public UnityEvent<BossController> hitBoss;
    
    public void SetDirection(Vector3 direction, float speed)
    {
        transform.position = new Vector3(transform.position.x, Y_LEVEL, transform.position.z);
        rb.linearVelocity = direction * speed;
        
        if (rb.linearVelocity == Vector3.zero)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        EnemyController enemy = other.GetComponent<EnemyController>();
        if(enemy)
            hitAnEnemy.Invoke(enemy);
        else
        {
            WitchPlayerController player = other.GetComponent<WitchPlayerController>();
            
            if (player)
                hitPlayer.Invoke(player);
            else
            {
                BossController boss = other.GetComponent<BossController>();
                if(boss)
                    hitBoss.Invoke(boss);
            }
        }
        Destroy(gameObject);
    }
}
