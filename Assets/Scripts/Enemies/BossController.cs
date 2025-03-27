/*using UnityEngine;
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
        if (player != null)
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
                    GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                    FireBoltScript newFirebolt = Instantiate(projectile, firePoint.transform.position, Quaternion.identity).GetComponent<FireBoltScript>();
                    newFirebolt.SetDirection(firePoint.transform.forward, bulletSpeed);
                    newFirebolt.hitATarget.AddListener(DealDamage);
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

    void Die()
    {
        // Handle death logic (animations, loot drops, etc.).
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
}*/


using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
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

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;  // Prefab must have a Rigidbody and a FireBoltScript component.
    public Transform leftFirePoint;
    public Transform middleFirePoint;
    public Transform rightFirePoint;
    public float projectileSpeed = 10f;
    public float shootIntervalMin = 3f;
    public float shootIntervalMax = 6f;

    [Header("Damage Events")]
    // Event that gets triggered when the boss deals damage.
    // It passes the PlayerHealth target and the damage amount.
    public UnityEvent<PlayerHealth, int> onDealDamage;

    void Start()
    {
        currentHP = maxHP;
        
        // Automatically assign the NavMeshAgent if it's not set.
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        // Start the projectile shooting routine.
        StartCoroutine(ShootingRoutine());
    }

    void Update()
    {
        // Boss follows the player.
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    IEnumerator ShootingRoutine()
    {
        while (currentHP > 0)
        {
            // Wait for a random time between shots.
            float waitTime = Random.Range(shootIntervalMin, shootIntervalMax);
            yield return new WaitForSeconds(waitTime);

            // Determine how many projectiles to fire (1 to 3).
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
            else // projectileCount == 3
            {
                chosenFirePoints = new Transform[] { leftFirePoint, middleFirePoint, rightFirePoint };
            }

            // Spawn a projectile from each selected fire point.
            foreach (Transform firePoint in chosenFirePoints)
            {
                if (firePoint != null)
                {
                    // Instantiate the projectile at the firePoint's position.
                    GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                    FireBoltScript firebolt = projectileInstance.GetComponent<FireBoltScript>();
                    
                    if (firebolt != null)
                    {
                        // Set the projectile's direction and speed.
                        firebolt.SetDirection(firePoint.forward, projectileSpeed);
                        // Listen for when the projectile hits a target.
                        firebolt.hitPlayer.AddListener(DealDamage);
                    }
                }
            }
        }
    }

    // This method is used by both projectile hits and collision events.
    private void DealDamage(PlayerHealth playerHealth)
    {
        // If any other systems need to know when the boss deals damage (for effects, sounds, etc.)
        // they can subscribe to onDealDamage.
        if (onDealDamage != null)
        {
            onDealDamage.Invoke(playerHealth, damage);
        }
        // Apply damage directly to the player's health.
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    // When the boss physically collides with the player, deal damage.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                DealDamage(playerHealth);
            }
        }
    }
    
    private void DealDamage(WitchPlayerController player)
    {
        player.TakeDamage(damage);
    }

    // Call this method to inflict damage on the boss.
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle death logic (e.g., playing an animation, dropping loot, etc.).
        Destroy(gameObject);
    }
}

