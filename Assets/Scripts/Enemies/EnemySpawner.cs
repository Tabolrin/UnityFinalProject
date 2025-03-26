using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("SpawnZone")]
    [Range(0, 360)][SerializeField] float startingAngle;
    [Range(0, 360)][SerializeField] float widthAngle;
    [SerializeField] float radius;
    [SerializeField] float minRadius;
    
    [Header("Spawnable Enemies")]
    [SerializeField] GameObject[] enemyArr;
    [Header("Refrences")]
    [SerializeField] WitchPlayerController player;

    void OnDrawGizmosSelected()
    {

#if UNITY_EDITOR
        Handles.color = Color.red;
        Handles.DrawSolidArc(transform.position, Vector3.up, AngleToVector(startingAngle), widthAngle, radius);
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        float angle = startingAngle - Random.Range(0, widthAngle);
        float distance = Random.Range(minRadius, radius);
        Vector3 enemySpawnPosition = (AngleToVector(angle) * distance) + transform.position;
        int enemyIndex = Random.Range(0, enemyArr.Length);

        EnemyController newEnemy = Instantiate(enemyArr[enemyIndex], enemySpawnPosition, Quaternion.identity).GetComponent<EnemyController>();
        newEnemy.player = player;
    }

    private Vector3 AngleToVector(float angle)
    {
        return new Vector3((float)Mathf.Cos(angle * Mathf.Deg2Rad), 0, (float)Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}
