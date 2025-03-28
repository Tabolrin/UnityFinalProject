using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("SpawnZone")]
    [Range(0, 360)][SerializeField] float startingAngle;
    [Range(0, 360)][SerializeField] float widthAngle;
    [SerializeField] float radius;
    
    float minRadius = 2;
    
    [Header("SpawnLogic")]
    [SerializeField] float distanceFromPlayerSpawning;
    [SerializeField] float spawnCooldown;
    [SerializeField] float spawnTimingRandomness;
    [SerializeField] int minimumEnemiesToSpawn;
    [SerializeField] int maximumEnemiesToSpawn;
    
    float spawnTime = 0;
    
    [Header("Spawnable Enemies")]
    [SerializeField] GameObject[] enemyArr;
    
    [Header("Refrences")]
    [SerializeField] WitchPlayerController player;
    [SerializeField] ParticleSystem particles;
    [SerializeField] ScoreManager scoreManager;

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawSolidArc(transform.position, Vector3.up, AngleToVector(startingAngle), widthAngle, radius);
    }
#endif
    private void Update()
    {
        float playerDistance = (player.transform.position - transform.position).magnitude;
        if (playerDistance <= distanceFromPlayerSpawning && Time.time > spawnTime)
        {
            particles.Play();
            int enemyAmount = Random.Range(minimumEnemiesToSpawn, maximumEnemiesToSpawn + 1);
            for (int i = 0; i < enemyAmount; i++)
                SpawnEnemy();
            spawnTime = Time.time + spawnCooldown + Random.Range(0, spawnTimingRandomness);
        }
    }

    public void SpawnEnemy()
    {
        float angle = startingAngle - Random.Range(0, widthAngle);
        float distance = Random.Range(minRadius, radius);
        Vector3 enemySpawnPosition = (AngleToVector(angle) * distance) + transform.position;
        int enemyIndex = Random.Range(0, enemyArr.Length);
        float randomAngle = Random.Range(0, 360);

        EnemyController newEnemy = Instantiate(enemyArr[enemyIndex], enemySpawnPosition, Quaternion.Euler(new Vector3(0, randomAngle, 0))).GetComponent<EnemyController>();
        newEnemy.player = player;
        newEnemy.scoreManager = scoreManager;
    }

    private Vector3 AngleToVector(float angle)
    {
        return new Vector3((float)Mathf.Cos(angle * Mathf.Deg2Rad), 0, (float)Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}
