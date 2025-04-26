using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    private Transform[] spawnPositionLimits;

    public List<EnemyDefinition> enemyDefinitions;
    public List<GameObject> enemiesPrefabs, currentWaveEnemies;
    public List<EnemyWave> enemyWaves;

    public EnemyWave currentEnemyWave;

    public List<float> enemiesPower;
    public float spawnRate;
    public AnimationCurve temperatureCurve;
    public float temperaturePercentage;

    private float randomXTransform;
    public float x1, x2;
    float currentTime = 0;
    void Start()
    {
        temperaturePercentage = temperatureCurve.Evaluate(Time.time);
        spawnPositionLimits = GetComponentsInChildren<Transform>();
        RandomizeEnemySpawnPosition();

        SpawnFirstWave();
        SpawnSecondWave();
        SpawnThirdWave();
        SpawnFourthWave();
        SpawnFifthWave();
        SpawnSixthWave();
        SpawnBoss();
    }
    public void RandomizeEnemySpawnPosition()
    {
        randomXTransform = Random.Range(spawnPositionLimits[1].position.x, spawnPositionLimits[2].position.x);
    }

    public GameObject SpawnEnemy(GameObject enemyPrefab, float xPosition = default)
    {
        if (xPosition != default)
        {
            randomXTransform = xPosition;
        }
        GameObject enemyInstance = Instantiate(enemyPrefab, new Vector3(randomXTransform, transform.position.y, transform.position.z), enemyPrefab.transform.rotation);

        RandomizeEnemySpawnPosition();

        return enemyInstance;
    }

    private IEnumerator SpawnEnemyAfterCoroutine(GameObject enemyPrefab, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject enemyInstance = Instantiate(enemyPrefab, new Vector3(randomXTransform, transform.position.y, transform.position.z), enemyPrefab.transform.rotation);
        RandomizeEnemySpawnPosition();
    }

    private void SpawnEnemyAfter(GameObject enemyPrefab, float delay)
    {
        StartCoroutine(SpawnEnemyAfterCoroutine(enemyPrefab, delay));
        StopCoroutine(SpawnEnemyAfterCoroutine(enemyPrefab, delay));
    }

    IEnumerator SpawnMultipleEnemiesAfterCoroutine(EnemyWave enemyWave)
    {
        yield return new WaitForSeconds(enemyWave.SecondsToStart);
        StartCoroutine(InstantiateMultipleWithDelay(enemyWave));
        StopCoroutine(InstantiateMultipleWithDelay(enemyWave));
    }

    private void SpawnMultipleEnemiesAfter(EnemyWave enemyWave)
    {
        StartCoroutine(SpawnMultipleEnemiesAfterCoroutine(enemyWave));
        StopCoroutine(SpawnMultipleEnemiesAfterCoroutine(enemyWave));
    }

    private List<GameObject> SpawnMultipleEnemies(EnemyWave enemyWave)
    {
        StartCoroutine(InstantiateMultipleWithDelay(enemyWave));
        StopCoroutine(InstantiateMultipleWithDelay(enemyWave));
        return currentWaveEnemies;
    }
    IEnumerator InstantiateMultipleWithDelay(EnemyWave enemyWave)
    {
        List<GameObject> spawnedEnemies = new List<GameObject>(); // declare a new List to store the spawned enemies

        GameObject enemyInstance;
        GameObject enemyPrefab = enemyWave.EnemyPrefab;
        int enemyAmount = enemyWave.EnemyAmount;
        float delay = enemyWave.DelayBetweenSpawns;
        float xPosition = enemyWave.XPosition;


        for (int i = 0; i < enemyAmount; i++)
        {
            enemyInstance = SpawnEnemy(enemyPrefab, xPosition);
            enemyInstance.GetComponent<EnemyController>().power = enemyWave.EnemyDropChance;
            currentWaveEnemies.Add(enemyInstance);
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnFirstWave()
    {
        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[1])
            .WithAmount(30)
            .WithDelay(0.7f)
            .WithSecondsToStart(0)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);
    }

    private void SpawnSecondWave(float secondsToStart = 21)
    {
        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[1])
            .WithAmount(25)
            .WithDelay(0.2f)
            .WithSecondsToStart(secondsToStart)
            .WithXPosition(0.01f)
            .WithEnemyDropChance(0.7f)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);
    }

    private void SpawnThirdWave(float secondsToStart = 30)
    {
        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[0])
            .WithAmount(6)
            .WithDelay(5)
            .WithSecondsToStart(secondsToStart)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);
    }

    private void SpawnFourthWave(float secondsToStart = 60)
    {
        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[0])
            .WithAmount(5)
            .WithDelay(6)
            .WithSecondsToStart(secondsToStart)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

        currentEnemyWave = new EnemyWaveBuilder()
        .WithPrefab(enemiesPrefabs[3])
        .WithAmount(5)
        .WithDelay(Random.Range(0.5f, 7f))
        .WithSecondsToStart(secondsToStart)
        .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

        currentEnemyWave = new EnemyWaveBuilder()
        .WithPrefab(enemiesPrefabs[1])
        .WithAmount(30)
        .WithDelay(1)
        .WithSecondsToStart(secondsToStart + 1.5f)
        .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);
    }

    private void SpawnFifthWave(float secondsToStart = 90)
    {
        // 10 segundos
        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[1])
            .WithAmount(16)
            .WithDelay(0.15f)
            .WithSecondsToStart(secondsToStart)
            .WithXPosition(-12)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

        currentEnemyWave = new EnemyWaveBuilder()
        .WithPrefab(enemiesPrefabs[1])
        .WithAmount(16)
        .WithDelay(0.15f)
        .WithSecondsToStart(secondsToStart)
        .WithXPosition(0.01f)
        .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[1])
            .WithAmount(16)
            .WithDelay(0.15f)
            .WithSecondsToStart(secondsToStart)
            .WithXPosition(12)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[0])
            .WithAmount(1)
            .WithDelay(0)
            .WithSecondsToStart(secondsToStart + 1.5f)
            .WithXPosition(-8)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[0])
            .WithAmount(1)
            .WithDelay(0)
            .WithSecondsToStart(secondsToStart + 1.5f)
            .WithXPosition(8)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);
    }

    private void SpawnSixthWave(float secondsToStart = 96)
    {
        // 10 segundos
        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[1])
            .WithAmount(16)
            .WithDelay(0.15f)
            .WithSecondsToStart(secondsToStart)
            .WithXPosition(-6)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[1])
            .WithAmount(16)
            .WithDelay(0.15f)
            .WithSecondsToStart(secondsToStart)
            .WithXPosition(6)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[0])
            .WithAmount(1)
            .WithDelay(0)
            .WithSecondsToStart(secondsToStart + 1.5f)
            .WithXPosition(-12)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

        currentEnemyWave = new EnemyWaveBuilder()
            .WithPrefab(enemiesPrefabs[0])
            .WithAmount(1)
            .WithDelay(0)
            .WithSecondsToStart(secondsToStart + 1.5f)
            .WithXPosition(0.01f)
            .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

        currentEnemyWave = new EnemyWaveBuilder()
             .WithPrefab(enemiesPrefabs[0])
             .WithAmount(1)
             .WithDelay(0)
             .WithSecondsToStart(secondsToStart + 1.5f)
             .WithXPosition(12)
             .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

    }

    // Randomly spawns a enemiesprefabs[3] in a random interval between 10 adn 20 seconds
    public void SpawnRandomEnemy(GameObject enemiePrefab, int amount = 1)
    {
        float secondsToStart = Random.Range(0, 15);
        currentEnemyWave = new EnemyWaveBuilder()
                .WithPrefab(enemiePrefab)
                .WithAmount(Random.Range(1, amount))
                .WithDelay(Random.Range(amount, amount * 5))
                .WithSecondsToStart(secondsToStart + 30)
                .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);
    }

    public void SpawnBoss(float secondsToStart = 110)
    {
        currentEnemyWave = new EnemyWaveBuilder()
                .WithPrefab(enemiesPrefabs[4])
                .WithAmount(1)
                .WithDelay(0)
                .WithSecondsToStart(secondsToStart)
                .WithXPosition(0.001f)
                .Build();
        SpawnMultipleEnemiesAfter(currentEnemyWave);

    }

    public void SpawnBossCheat(){
        SpawnEnemy(enemiesPrefabs[4], 0.001f);
    }


}