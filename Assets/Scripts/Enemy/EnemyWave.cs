using UnityEngine;

public class EnemyWaveBuilder
{
    private GameObject _enemyPrefab;
    private int _enemyAmount;
    private float _delayBetweenSpawns;
    private float _xPosition;
    private float _secondsToStart;
    private float _enemyDropChance;

    public EnemyWaveBuilder WithPrefab(GameObject enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;
        return this;
    }

    public EnemyWaveBuilder WithAmount(int enemyAmount)
    {
        _enemyAmount = enemyAmount;
        return this;
    }

    public EnemyWaveBuilder WithDelay(float delayBetweenSpawns)
    {
        _delayBetweenSpawns = delayBetweenSpawns;
        return this;
    }

    public EnemyWaveBuilder WithXPosition(float xPosition)
    {
        _xPosition = xPosition;
        return this;
    }

    public EnemyWaveBuilder WithSecondsToStart(float secondsToStart)
    {
        _secondsToStart = secondsToStart;
        return this;
    }

    public EnemyWaveBuilder WithEnemyDropChance(float enemyDropChance)
    {
        _enemyDropChance = enemyDropChance;
        return this;
    }

    public EnemyWave Build()
    {
        return new EnemyWave(_enemyPrefab, _enemyAmount, _delayBetweenSpawns, _xPosition, _secondsToStart, _enemyDropChance);
    }
}

public class EnemyWave 
{
    private GameObject _enemyPrefab;
    private int _enemyAmount;
    private float _delayBetweenSpawns;
    private float _xPosition;
    private float _secondsToStart;
    private float _enemyDropChance;

    public EnemyWave(GameObject prefab, int amount, float delay, float xPos, float secondsToStart, float enemyDropChance)
    {
        _enemyPrefab = prefab;
        _enemyAmount = amount;
        _delayBetweenSpawns = delay;
        _xPosition = xPos;
        _secondsToStart = secondsToStart;
        _enemyDropChance = enemyDropChance;
    }

    public GameObject EnemyPrefab { get { return _enemyPrefab; } }
    public int EnemyAmount { get { return _enemyAmount; } }
    public float DelayBetweenSpawns { get { return _delayBetweenSpawns; } }
    public float XPosition { get { return _xPosition; } }
    public float SecondsToStart { get { return _secondsToStart; } }
    public float EnemyDropChance { get { return _enemyDropChance; } }
}
