using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public  bool isPaused = false;
    public GameOver gameOver;
    public GameObject gameManager;

    private void Start()
    {
        gameOver = FindObjectOfType<GameOver>(true);
        gameManager = GameObject.Find("GameManager");
    }


        // Pause the game
    public  void PauseGame()
    {
        if (CheckGameOver())
        {
            return;
        }
        isPaused = true;
        Time.timeScale = 0f;
        gameObject.SetActive(true);

    }

    // When player presses ESC, pauses the game
    public  void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    // Check if GameOver is active
    public bool CheckGameOver()
    {
        if (gameOver.gameObject.activeSelf)
        {
            return true;
        }
        return false;
    }

    public void BackToMenu()
    {
        gameManager.GetComponent<SceneManagerCustom>().LoadScene0();
    }

    

}
