using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("Boss Stats")]
    public int maxHP = 30;
    private int currentHP;
    public int damage = 2;

    [Header("Movement Settings")]
    public NavMeshAgent agent;
    public Transform player;
    public Animator anim;
    const string deathTrigger = "DeathAnimation";

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;  // Prefab must have a Rigidbody component.
    public Transform leftFirePoint;
    public Transform middleFirePoint;
    public Transform rightFirePoint;
    public float projectileSpeed = 10f;
    public float shootIntervalMin = 3f;
    public float shootIntervalMax = 6f;

    void Start()
    {
        currentHP = maxHP;
        
        // Automatically get the NavMeshAgent if not already assigned.
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        // Begin the shooting routine.
        StartCoroutine(ShootingRoutine());
    }

    void Update()
    {
        // Boss follows the player's position.
        if (player != null && agent.enabled)
        {
            agent.SetDestination(player.position);
        }
    }

    IEnumerator ShootingRoutine()
    {
        while (currentHP > 0)
        {
            float waitTime = Random.Range(shootIntervalMin, shootIntervalMax);
            yield return new WaitForSeconds(waitTime);


            int projectileCount = Random.Range(1, 4);
            Transform[] chosenFirePoints;
            
            if (projectileCount == 1)
            {
                chosenFirePoints = new Transform[] { middleFirePoint };
            }
            else if (projectileCount == 2)
            {
                chosenFirePoints = new Transform[] { leftFirePoint, rightFirePoint };
            }
            else
            {
                chosenFirePoints = new Transform[] { leftFirePoint, middleFirePoint, rightFirePoint };
            }

            // Instantiate projectiles from each chosen fire point.
            foreach (Transform firePoint in chosenFirePoints)
            {
                if (firePoint != null)
                {
                    // Spawn the projectile at the firePoint's position and rotation.
                    //GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                    FireBoltScript newFirebolt = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity).GetComponent<FireBoltScript>();
                    newFirebolt.SetDirection(firePoint.transform.forward, projectileSpeed);
                    newFirebolt.hitPlayer.AddListener(DealDamage);
                }
            }
        }
    }

    // Call this method from other scripts to damage the boss.
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    
    private void DealDamage(WitchPlayerController player)//---------------------------------------alter to event?
    {
        player.TakeDamage(damage);
        Debug.Log($"Player should have taken {damage} damage");
    }

    void Die()
    {
        // Handle death logic (animations, loot drops, etc.).
        agent.enabled = false;
        anim.SetTrigger(deathTrigger);
    }
    //An event set on the model object with the animator will call this as an animation event on the last frame of the animation
    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    // When the boss collides with the player, deal damage.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
