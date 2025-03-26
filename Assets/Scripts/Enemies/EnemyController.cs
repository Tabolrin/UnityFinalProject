using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GoToPoint goToPoint;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;
    public WitchPlayerController player;
    float StillThreshold = 0.05f;
    float knockbackStun = 0.25f;


    public int ContactDamage { get; private set; } = 10;
    int hp = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goToPoint.GoToTarget(player.transform.position);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Destroy(gameObject);
    }

    public IEnumerator Knockback(Vector3 force)
    {
        yield return null;
        agent.enabled = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(force);

        yield return new WaitForFixedUpdate();
        float knockbackTime = Time.time;
        yield return new WaitUntil(
            () => rb.linearVelocity.magnitude < StillThreshold
            );
        //yield return new WaitForSeconds(knockbackStun);

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        agent.Warp(transform.position);
        agent.enabled = true;

        yield return null;
    }
}
