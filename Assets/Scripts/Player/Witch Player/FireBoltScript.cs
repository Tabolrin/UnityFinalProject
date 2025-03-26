using Unity.VisualScripting;
using UnityEngine;

public class FireBoltScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    
    public void SetDirection(Vector2 direction, float speed)
    {
        Vector2 vect2D = direction * speed;
        rb.linearVelocity = new Vector3(vect2D.x, 0, vect2D.y);
        if(rb.linearVelocity == Vector3.zero)
            Destroy(gameObject);
    }
}
