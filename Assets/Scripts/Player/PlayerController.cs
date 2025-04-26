using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float maxHealth;
    public float currentHealth;
    public float damage;
    public float rotationSpeed = 0.5f;
    public float tiltAngle = 45f;
    public float horizontalInput;
    public float verticalInput;
    public float healthRegeneration = 0;
    public Quaternion originalRotation;

    // Bubble Shield
    private bool haveShieldBubble = false;
    private float shieldBubbleDuration = 10f;
    private float shieldBubbleDurationCounter;
    private float bubbleShieldMaxHealth = 3f;

    private float bubbleShieldHealth = 1f;
    private float bubbleShieldHealthPercentage;
    private float bubbleShieldStartingOpacity = 0.15f;

    private Rigidbody rb;

    private PlayerInput playerInput;



    // References
    private GunSystem gunSystem;
    public GameObject shieldBubble;
    private GameObject bubbleShieldInstance;
    [SerializeField]
    private TextMeshProUGUI buffText, healthText;
    [SerializeField]
    private GameObject gameOverPanel;

    private EmissionController emissionController;
    private GameManager gameManager;
    private Color oldColor, blinkColor;


    void Start()
    {
        StopAllCoroutines();
        currentHealth = maxHealth;
        gunSystem = GetComponent<GunSystem>();
        gunSystem.damage = damage;
        healthText.text = "Health: " + currentHealth.ToString();
        ChangeHealthTextColor();
        playerInput = GetComponent<PlayerInput>();
        rb = gameObject.GetComponent<Rigidbody>();
        originalRotation = transform.rotation;
        emissionController = GetComponent<EmissionController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.StopAllCoroutines();
    }

    void FixedUpdate()
    {
       TiltShip();

    }

    public void TakeDamage(float damage)
    {
        if (haveShieldBubble)
        {
            TakeDamageOnBubbleShield(damage);
            return;
        }

        TakeDamageOnHealth(damage);


    }

    private void TakeDamageOnBubbleShield(float damage)
    {

        bubbleShieldHealth -= damage;
        bubbleShieldHealthPercentage = bubbleShieldHealth / bubbleShieldMaxHealth;

        if (bubbleShieldHealth <= 0)
        {
            DestroyBubbleShield();
        }
    }


    public float IncreaseHealth(float health)
    {
        currentHealth += health;
        ChangeHealthTextColor();
        healthText.text = "Health: " + currentHealth.ToString();

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        return currentHealth;
    }

    public float IncreaseMaxHealth(float health)
    {
        maxHealth += health;
        ChangeHealthTextColor();
        healthText.text = "Health: " + currentHealth.ToString();
        return maxHealth;
    }


    public void IncreaseMovementSpeed(float movementSpeed)
    {
        this.movementSpeed *= movementSpeed;
    }

    // Instantiate shield bubble inside player
    public void ActivateBubbleShield()
    {
        bubbleShieldHealth = bubbleShieldMaxHealth;
        if (haveShieldBubble)
        {
            shieldBubbleDurationCounter = shieldBubbleDuration;
            return;
        }
        bubbleShieldInstance = Instantiate(shieldBubble, transform.position, Quaternion.identity);
        bubbleShieldInstance.transform.parent = transform;
        bubbleShieldInstance.transform.localPosition = new Vector3(0, -8.8f, 0);
        haveShieldBubble = true;
        StopCoroutine(ShieldBubbleTimer());
        StartCoroutine(ShieldBubbleTimer());

    }

    IEnumerator ShieldBubbleTimer()
    {
        yield return new WaitForSeconds(shieldBubbleDuration);
        DestroyBubbleShield();
    }

    private void DestroyBubbleShield()
    {
        Destroy(bubbleShieldInstance);
        haveShieldBubble = false;
    }

    // Change Bubble shield material instance alpha/opacity when taking damage
    private void ChangeBubbleShieldOpacity()
    {
        Material bubbleShieldMaterial = bubbleShieldInstance.GetComponent<Renderer>().material;
        Color bubbleShieldColor = bubbleShieldMaterial.color;
        bubbleShieldColor.a = bubbleShieldHealthPercentage * bubbleShieldStartingOpacity;
        bubbleShieldMaterial.color = bubbleShieldColor;
    }

    // Change buff text according to buff type
    public void ChangeBuffText(string buffType)
    {
        AppearBuffText();
        buffText.text = buffType;
        FadeBuffText();
    }

    // Makes buff text slowly fade away
    public void FadeBuffText()
    {
        buffText.CrossFadeAlpha(0, 2, false);
    }

    // Makes buff text appear again
    public void AppearBuffText()
    {
        buffText.CrossFadeAlpha(1, 0, false);
    }

    // Change health text color as player takes damage
    public void ChangeHealthTextColor()
    {
        if (currentHealth <= 0.4 * maxHealth)
        {
            healthText.color = Color.red;
        }
        else if (currentHealth <= 0.7 * maxHealth)
        {
            healthText.color = Color.yellow;
        }
        else
        {
            healthText.color = Color.green;
        }
    }

    public void Blink(Color color = default){
        blinkColor = color;
        StopCoroutine(BlinkCoroutine());
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine(){
        oldColor = blinkColor;
        emissionController.SetColorAndIntensity(blinkColor, 30);
        for (int i = 100; i > 0; i--){
            float emissao = i/3;
            if (emissao < 1.2f) emissao = 1.2f;
            emissionController.SetColorAndIntensity(blinkColor, emissao);
            yield return new WaitForSeconds(0.001f);
            
        }
        emissionController.SetColor(emissionController.initialEmissiveColor);
    }



    // Turn player  gameObject Inactive and pause game

    public void DieAndPause()
    {
        gameManager.GetComponent<SoundManager>().PlaySFX(3);
        DeactivatePlayer();
        gameOverPanel.SetActive(true);
        gameManager.SlowTime();

    }

    public void TakeDamageOnHealth(float damage){
        //Blink();

        currentHealth -= damage;
        ChangeHealthTextColor();
        healthText.text = "Health: " + currentHealth.ToString();
        if (currentHealth <= 0)
        {
            DieAndPause();
        }
    }


    private void TiltShip(){
        if (rb.velocity.magnitude > 0.1)
        {
            float tiltAroundZ = -horizontalInput * tiltAngle;
            Quaternion targetRotation = Quaternion.Euler(originalRotation.eulerAngles.x, originalRotation.eulerAngles.y, -tiltAroundZ);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * rotationSpeed * 5);
        
        }
    }

    // Deatctivates player and its children visibility and collider
    private void DeactivatePlayer()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }




}
