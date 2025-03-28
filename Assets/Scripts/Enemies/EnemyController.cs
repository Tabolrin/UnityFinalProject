using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float visionDistance;
    [SerializeField] float attackRange;
    [SerializeField] float projectileSpeed;

    [Header("Refrences")]
    [SerializeField] GoToPoint goToPoint;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject shootPoint;
    public WitchPlayerController player;
    public ScoreManager scoreManager;

    //Fields
    float spawnEndTime;

    //consts
    const float StillThreshold = 0.05f;
    const float spawnAnimation = 3;
    const string walkingBool = "Walk";
    const string shootTrigger = "Shoot";
    const string runningAnimation = "Running_A";


    public int ContactDamage { get; private set; } = 10;
    int hp = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnEndTime = spawnAnimation + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled && Time.time > spawnEndTime)
        {
            float playerDistance = (player.transform.position - transform.position).magnitude;
            
            if (playerDistance < attackRange)
            {
                anim.SetBool(walkingBool, false);
                agent.isStopped = true;
                transform.LookAt(player.transform.position);
                anim.SetTrigger(shootTrigger);
            }
            else if (playerDistance <= visionDistance)
            {
                agent.isStopped = false;
                anim.SetBool(walkingBool, true);
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(runningAnimation))
                    goToPoint.GoToTarget(player.transform.position);
            }
            else
            {
                anim.SetBool(walkingBool, false);
                agent.isStopped = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        AudioManager.Instance.PlaySound(AudioManager.SoundClips.EnemyHurtSfx);
        hp -= damage;
        
        if (hp <= 0)
        {
            scoreManager.AddScore(5);
            
            Destroy(gameObject);
            return;
        }
        Vector3 knockbackVect = (transform.position - player.transform.position).normalized * player.knockbackPower;
        StartCoroutine(Knockback(knockbackVect));
    }

    public void CallKnockBack(Vector3 force)
    {
        StartCoroutine(Knockback(force));
    }

    private IEnumerator Knockback(Vector3 force)
    {
        yield return null;
        agent.enabled = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(force);

        yield return new WaitForFixedUpdate();
        float knockbackTime = Time.time;
        
        yield return new WaitUntil
        (
            () => rb.linearVelocity.magnitude < StillThreshold
        );
        //yield return new WaitForSeconds(knockbackStun);

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        yield return null;
        yield return null;
        rb.useGravity = false;
        rb.isKinematic = true;
        agent.Warp(transform.position);
        agent.enabled = true;

        yield return null;
    }

    public void SpawnProjectile()
    {
        if (projectile == null) return;
        
        FireBoltScript bolt = Instantiate(projectile, shootPoint.transform.position, transform.rotation).GetComponent<FireBoltScript>();
        bolt.SetDirection(transform.forward, projectileSpeed);
        bolt.hitPlayer.AddListener(DealDamage);
    }
    private void DealDamage(WitchPlayerController player)
    {
        player.TakeDamage(ContactDamage);
    }
}
