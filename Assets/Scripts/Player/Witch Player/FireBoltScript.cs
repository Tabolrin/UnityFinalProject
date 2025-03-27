using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FireBoltScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public UnityEvent<EnemyController> hitATarget;
    private const float Y_LEVEL = 1;
    
    public void SetDirection(Vector3 direction, float speed)
    {
        transform.position = new Vector3(transform.position.x, Y_LEVEL, transform.position.z);
        rb.linearVelocity = direction * speed;
        
        if (rb.linearVelocity == Vector3.zero)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") 
            || other.gameObject.CompareTag("EnemyProjectile")
            || other.gameObject.CompareTag("Boss")) return;
        
        EnemyController enemy;
        enemy = other.GetComponent<EnemyController>();
        
        if(enemy != null)
            hitATarget.Invoke(enemy);
        Destroy(gameObject);
    }
}
