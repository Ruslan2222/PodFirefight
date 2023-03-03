using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject[] enemies;
    [Space]
    [Header("Spawn Settings")]
    [Space]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float initialSpawnTime;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float spawnTimeDecrease;
    [SerializeField] private int initialEnemiesPerSpawn;
    [SerializeField] private int maxEnemiesPerSpawn;
    [SerializeField] private float spawnHeight;

    private float blueToRedRatio = 0.2f;
    private float nextSpawnTime = 0;
    private float currentSpawnTime;
    private int currentEnemiesPerSpawn;

    private void Start()
    {
        currentSpawnTime = initialSpawnTime;
        currentEnemiesPerSpawn = initialEnemiesPerSpawn;
        nextSpawnTime = Time.time + currentSpawnTime;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            float randomValue = Random.value;

            if (randomValue < blueToRedRatio)
            {
                SpawnEnemy(enemies[0]);
            }
            else
            {
                SpawnEnemy(enemies[1]);
            }

            currentSpawnTime -= spawnTimeDecrease;
            currentSpawnTime = Mathf.Max(currentSpawnTime, minSpawnTime);

            if (currentSpawnTime == 2)
            {
                currentEnemiesPerSpawn = Mathf.Min(currentEnemiesPerSpawn + 1, maxEnemiesPerSpawn);
            }

            nextSpawnTime = Time.time + currentSpawnTime;
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPosition = spawnPoint.position + new Vector3(Random.insideUnitCircle.x * 8, 0);
        //spawnPosition.x = Mathf.Max(spawnPosition.x, spawnPoint.position.x);

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        enemy.transform.parent = transform;
    }

}
