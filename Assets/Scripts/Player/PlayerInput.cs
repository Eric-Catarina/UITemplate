using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float speed = 60;
    
    private Rigidbody rb;

    [SerializeField] private float speedModifier = 1.05f;

    private GunSystem gunSystem;
    private GameManager gameManager;
    private PlayerController playerController;
    private PauseMenu pauseMenu;

    public float horizontalPlayerInput, verticalPlayerInput;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        gunSystem = GetComponent<GunSystem>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GetComponent<PlayerController>();
        pauseMenu = FindObjectOfType<PauseMenu>(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.isPaused)
        {
            pauseMenu.PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.isPaused)
        {
            pauseMenu.UnpauseGame();
        }

        MoveShip(horizontalPlayerInput, verticalPlayerInput);
        
        if(gunSystem.allowButtonHold)
        {
            gunSystem.tryingToShoot = Input.GetKey("space");
        }

        else
        {
            gunSystem.tryingToShoot = Input.GetKeyDown("space");
        }
    }   
    void  MoveShip(float horizontalPlayerInput, float verticalPlayerInput)
    {
        rb.velocity = Vector3.zero;
        horizontalPlayerInput = Input.GetAxisRaw("Horizontal");
        verticalPlayerInput = Input.GetAxisRaw("Vertical");
        
        playerController.horizontalInput = horizontalPlayerInput;
        playerController.verticalInput = verticalPlayerInput;

        if (horizontalPlayerInput != 0 || verticalPlayerInput != 0)
        {
            rb.velocity = (new Vector3(horizontalPlayerInput, verticalPlayerInput, 0).normalized * (speed * Time.fixedDeltaTime));

        }
    }

    
    public void IncreaseSpeed()
    {
        speed *= speedModifier;
    }
}