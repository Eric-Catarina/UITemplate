using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemyBehaviour")]
public class EnemyDefinition : ScriptableObject
{
    public float currentHealth, maxHealth;

    public float movementSpeed;

    public bool movesInSin;
    public bool lookAtPlayer;
    public bool movesTowardPlayer;
    public bool shoots;

    public float power;

    public float sinFrequency, sinAmplitude;
    public float damage;
    public List<GameObject> possibleItemDrops;
    public float[] rarities;
}
