using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FireBoltScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public UnityEvent<EnemyController> hitATarget;
    const float yLevel = 1;
    
    public void SetDirection(Vector2 direction, float speed)
    {
        transform.position = new Vector3(transform.position.x, yLevel, transform.position.z);
        Vector2 vect2D = direction * speed;
        rb.linearVelocity = new Vector3(vect2D.x, 0, vect2D.y);
        if(rb.linearVelocity == Vector3.zero)
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
