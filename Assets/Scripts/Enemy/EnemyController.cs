using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    // References
    private GameObject scoreManager;
    private ScoreManager scoreManagerScript;

    [SerializeField]
    private GameObject item;

    public EnemyDefinition enemyDefinition;
    public EmissionController emissionController;
    private GameObject player;
    public Vector3 initialDirection, playerInitialPosition;

    public GameObject bullet;

    public GameManager gameManager;

    // Health
    private float currentHealth;
    private float maxHealth;
    private bool estaMorto = false;

    // Movement
    private float movementSpeed;
    public float damage;
    private bool movesInSin;
    private bool lookAtPlayer;
    public bool movesTowardPlayer;
    private bool shoots;
    private float sinCenterX;
    private float amplitude;
    private float frequency;

    public float power;

    private Rigidbody rb;
    private float[] rarities;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        sinCenterX = transform.position.x;

        player = GameObject.Find("Player");
        playerInitialPosition = player.transform.position;
        initialDirection = (playerInitialPosition - transform.position).normalized;
        InitializeEnemyDefinition(enemyDefinition);

        if(movesTowardPlayer){
            SetInitialZRotation(initialDirection);
        }
        scoreManagerScript = (ScoreManager)FindObjectOfType(typeof(ScoreManager));
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));

        if (shoots)
        {
            StartCoroutine(ShootCoroutine());
        }

    }

    void FixedUpdate()
    {
        if (lookAtPlayer)
        {
            LookAtPlayer();
        }

        if (movesInSin)
        {

            float x = Mathf.Sin(Time.time * frequency) * amplitude;
            float y = Mathf.Abs(Mathf.Cos(Time.time * frequency) * amplitude);
            Vector3 direction = new Vector3(x, -y, 0f);
            // convert the above rb.velocity to rb.MovePosition
            rb.MovePosition(transform.position + direction.normalized * movementSpeed * Time.fixedDeltaTime);

        }
        else
        {
            rb.velocity = Vector3.down * movementSpeed;
            if (movesTowardPlayer)
            {
                MoveTowardsPlayer();
            }
        }




    }
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.TryGetComponent(out BulletController bulletController))
        {
            TakeDamage(bulletController.damage);
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "Player")
        {
            PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(1);
        }
        else if (collider.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }

    }

    // Initialize the definition variables
    public void InitializeEnemyDefinition(EnemyDefinition enemyDefinition)
    {
        movementSpeed = enemyDefinition.movementSpeed;
        movesInSin = enemyDefinition.movesInSin;
        amplitude = enemyDefinition.sinAmplitude;
        frequency = enemyDefinition.sinFrequency;
        maxHealth = enemyDefinition.maxHealth;
        currentHealth = enemyDefinition.currentHealth;
        damage = enemyDefinition.damage;
        lookAtPlayer = enemyDefinition.lookAtPlayer;
        movesTowardPlayer = enemyDefinition.movesTowardPlayer;
        power = enemyDefinition.power;
        shoots = enemyDefinition.shoots;
        rarities = enemyDefinition.rarities;

    }

    private void Die()
    {
        if (!estaMorto)
        {
            if (Random.Range(0f, 100f) < power)
            {
                SpawnItem();
            }
            scoreManagerScript.AddScore(RandomNumber(100, 500));
            gameManager.SpawnExplosionEffect(transform.position);
            Destroy(gameObject);
        }
        estaMorto = true;
    }

    private void SpawnItem()
    {
        GameObject itemInstance = Instantiate(item, transform.position, transform.rotation);
        itemInstance.GetComponent<BuffItem>().rarity = GetRandomRarity(rarities);
    }

    public float TakeDamage(float damage)
    {

        currentHealth -= damage;
        scoreManagerScript.AddScore(RandomNumber(Mathf.RoundToInt(damage * 0.8f), Mathf.RoundToInt(damage * 1.2f)));

        if (currentHealth <= 0)
        {
            Die();
        }
        return currentHealth;
    }

    public void Blink()
    {
        StopCoroutine(BlinkCoroutine());
        emissionController.SetIntensity(1.2f);
        StartCoroutine(BlinkCoroutine());

    }

    private IEnumerator BlinkCoroutine()
    {
        emissionController.SetIntensity(0.0001f);

        for (int i = 10; i > 0; i--)
        {
            float emissao = i / 1.7f;
            if (emissao < 1.2f)
            {
                emissao = 1.2f;
            }
            emissionController.SetIntensity(emissao);
            yield return new WaitForSeconds(0.001f);
        }

    }

    // Smoothly rotates toward player position using rigidbody rotation in x and y axis
    private void LookAtPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 2 * Time.deltaTime);
    }
    private void MoveTowardsPlayer()
    {
        rb.velocity = initialDirection * movementSpeed;
    }

    public void SetInitialZRotation(Vector3 direction)
    {
        // Calculate the angle between Vector3.up and the target direction around the Z-axis
        float angle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward);

        // Set the transform's rotation, preserving the current X and Y rotations
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle + 180);
    }


    // Shoots at player
    public void Shoot()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
        GameObject bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
        BulletController bulletController = bulletInstance.GetComponent<BulletController>();
        bulletController.isEnemyBullet = true;
        bulletController.damage = damage;
    }

    // Shoot every 1.5 seconds
    public IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(1.5f);
        }
    }

    // Generate random number between function parameters
    public float RandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    public BuffItem.Rarity GetRandomRarity(float[] rarities)
    {
        // Get the total sum of rarities percentages
        float totalPercentage = 0;
        foreach (float rarityPercentage in rarities)
        {
            totalPercentage += rarityPercentage;
        }

        // Generate a random number between 0 and the total percentage
        float randomPercentage = Random.Range(0f, totalPercentage);

        // Check the random number against each rarity percentage
        float cumulativePercentage = 0;
        for (int i = 0; i < rarities.Length; i++)
        {
            cumulativePercentage += rarities[i];
            if (randomPercentage <= cumulativePercentage)
            {
                // Return the corresponding rarity
                return (BuffItem.Rarity)i;
            }
        }

        // This line is reached if the random percentage is greater than the total percentage
        // Return the highest rarity as a fallback
        return BuffItem.Rarity.Legendary;
    }


}
