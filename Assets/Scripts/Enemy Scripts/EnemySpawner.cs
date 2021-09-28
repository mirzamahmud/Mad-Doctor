using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField]
    private GameObject enemyPrefab; // so the enemy is going to spawn

    private GameObject newEnemy;

    [SerializeField]
    private Transform[] spawnPosition;

    [SerializeField]
    private int enemySpawnLimit = 6;

    [SerializeField]
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField]
    private float minimumSpawnTime = 2f, maximumSpawnTime = 5f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this; // reference for the instance
        }
    }

    private void Start()
    {
        Invoke("SpawnEnemy", Random.Range(minimumSpawnTime, maximumSpawnTime));
    }

    void SpawnEnemy()
    {
        Invoke("SpawnEnemy", Random.Range(minimumSpawnTime, maximumSpawnTime));

        // if we have 10 enemies into our game, sothat we don't spawn enemies
        if (spawnedEnemies.Count == enemySpawnLimit)
        {
            return;
        }

        newEnemy = Instantiate(enemyPrefab, spawnPosition[Random.Range(0, spawnPosition.Length)].position, Quaternion.identity);

        spawnedEnemies.Add(newEnemy);

    }

    public void EnemyDied(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
    }
}
