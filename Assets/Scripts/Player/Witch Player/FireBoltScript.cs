using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FireBoltScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public UnityEvent<EnemyController> hitATarget;
    const float yLevel = 1;
    
    public void SetDirection(Vector3 direction, float speed)
    {
        transform.position = new Vector3(transform.position.x, yLevel, transform.position.z);
        rb.linearVelocity = direction * speed;
        if (rb.linearVelocity == Vector3.zero)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy;
        enemy = other.GetComponent<EnemyController>();
        if(enemy != null)
            hitATarget.Invoke(enemy);
        Destroy(gameObject);
    }
}
