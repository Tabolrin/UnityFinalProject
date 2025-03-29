using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("Boss Stats")]
    public int maxHP = 30;
    private int _currentHP;
    private readonly int _damage = 10;

    [Header("Movement Settings")]
    public NavMeshAgent agent;
    public Transform player;

    [Header("Shooting Settings")]
    public GameObject projectilePrefab; 
    public Transform leftFirePoint;
    public Transform middleFirePoint;
    public Transform rightFirePoint;
    public float projectileSpeed = 10f;
    public float shootIntervalMin = 3f;
    public float shootIntervalMax = 6f;
    
    [Header("Refrences")]
    [SerializeField] SceneHandler sceneHandler;
    [SerializeField] GameObject finishLevel;
    [SerializeField] ScoreManager scoreManager;

    void Start()
    {
        _currentHP = maxHP;
        
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        
        StartCoroutine(ShootingRoutine());
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    IEnumerator ShootingRoutine()
    {
        while (_currentHP > 0)
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

            // Instantiate projectiles from relevant fire points.
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
            
            AudioManager.Instance.PlaySound(AudioManager.SoundClips.BossAttackSfx);
        }
    }
    
    public void TakeDamage(int amount)
    {
        AudioManager.Instance.PlaySound(AudioManager.SoundClips.EnemyHurtSfx);
        _currentHP -= amount;
        
        if (_currentHP <= 0)
        {
            Die();
        }
    }
    
    private void DealDamage(WitchPlayerController player)
    {
        player.TakeDamage(_damage);
        //Debug.Log($"Player should have taken {damage} damage");
    }

    private void Die()
    {
        scoreManager.AddScore(30);
        AudioManager.Instance.PlaySound(AudioManager.SoundClips.LevelWinSfx);
        finishLevel.SetActive(true);
        
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_damage);
            }
        }
    }
}
